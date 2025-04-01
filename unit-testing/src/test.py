import unittest
from unittest.mock import MagicMock
from flask_todo import app, tasks, task_id_counter
from bank_account import BankAccount
import json
from flight import BookingSystem, Flight, PaymentGateway


class TestBankAccount(unittest.TestCase):
    def setUp(self):
        self.account = BankAccount("Alice", 100.0)

    def test_initial_balance(self):
        self.assertEqual(self.account.get_balance(), 100.0)
        self.assertEqual(self.account.owner, "Alice")
        self.assertEqual(self.account.get_transaction_history(), [])

    def test_negative_initial_balance_raises_error(self):
        with self.assertRaises(ValueError) as context:
            BankAccount("Bob", -50.0)
        self.assertEqual(
            str(context.exception), "Startguthaben kann nicht negativ sein"
        )

    def test_deposit_positive_amount(self):
        self.account.deposit(50.0)
        self.assertAlmostEqual(self.account.get_balance(), 150.0)
        self.assertIn("Einzahlung: +50.00 EUR", self.account.get_transaction_history())

    def test_deposit_non_positive_amount_raises_error(self):
        with self.assertRaises(ValueError) as context_zero:
            self.account.deposit(0)
        self.assertEqual(
            str(context_zero.exception), "Einzahlungsbetrag muss positiv sein"
        )

        with self.assertRaises(ValueError) as context_negative:
            self.account.deposit(-10)
        self.assertEqual(
            str(context_negative.exception), "Einzahlungsbetrag muss positiv sein"
        )

    def test_withdraw_positive_amount_with_sufficient_balance(self):
        self.account.withdraw(30.0)
        self.assertAlmostEqual(self.account.get_balance(), 70.0)
        self.assertIn("Abhebung: -30.00 EUR", self.account.get_transaction_history())

    def test_withdraw_non_positive_amount_raises_error(self):
        with self.assertRaises(ValueError) as context_zero:
            self.account.withdraw(0)
        self.assertEqual(str(context_zero.exception), "Abhebebetrag muss positiv sein")

        with self.assertRaises(ValueError) as context_negative:
            self.account.withdraw(-20)
        self.assertEqual(
            str(context_negative.exception), "Abhebebetrag muss positiv sein"
        )

    def test_withdraw_insufficient_balance_raises_error(self):
        with self.assertRaises(ValueError) as context:
            self.account.withdraw(150.0)
        self.assertEqual(str(context.exception), "Nicht genug Guthaben verfügbar")

    def test_transaction_history_order(self):
        self.account.deposit(20)
        self.account.withdraw(10)
        expected_history = [
            "Einzahlung: +20.00 EUR",
            "Abhebung: -10.00 EUR",
        ]
        self.assertEqual(self.account.get_transaction_history(), expected_history)


class TestFlight(unittest.TestCase):

    def setUp(self):
        self.flight = Flight("LH123", 3)

    def test_initial_available_seats(self):
        self.assertEqual(self.flight.get_available_seats(), 3)

        seat_number = self.flight.book_seat("Alice")
        self.assertEqual(seat_number, 1)
        self.assertEqual(self.flight.get_available_seats(), 2)
        self.assertEqual(self.flight.reservations[1], "Alice")

        seat_number = self.flight.book_seat("Bob")
        self.assertEqual(seat_number, 2)
        self.assertEqual(self.flight.get_available_seats(), 1)
        self.assertEqual(self.flight.reservations[2], "Bob")

    def test_book_seat_no_available_seats(self):
        self.flight.book_seat("Alice")
        self.flight.book_seat("Bob")
        self.flight.book_seat("Charlie")
        self.assertEqual(self.flight.get_available_seats(), 0)
        with self.assertRaises(ValueError) as context:
            self.flight.book_seat("David")
        self.assertEqual(str(context.exception), "Keine Sitzplätze mehr verfügbar")


class TestPaymentGateway(unittest.TestCase):

    def setUp(self):
        self.gateway = PaymentGateway()

    def test_process_payment_valid_card(self):
        results = [
            self.gateway.process_payment(100, "4123456789012345") for _ in range(10)
        ]
        for result in results:
            self.assertIn(result, [True, False])

    def test_process_payment_invalid_card_raises_error(self):
        with self.assertRaises(ValueError) as context:
            self.gateway.process_payment(100, "5123456789012345")
        self.assertEqual(str(context.exception), "Ungültige Kreditkarte")


class TestBookingSystem(unittest.TestCase):

    def setUp(self):
        self.mock_gateway = MagicMock()
        self.booking_system = BookingSystem(self.mock_gateway)
        self.flight = Flight("LH123", 2)
        self.booking_system.add_flight(self.flight)

    def test_book_ticket_success(self):
        self.mock_gateway.process_payment.return_value = True

        seat_number = self.booking_system.book_ticket(
            "LH123", "Alice", "4123456789012345", 150
        )
        self.assertEqual(seat_number, 1)
        self.assertEqual(self.flight.get_available_seats(), 1)
        self.assertEqual(len(self.booking_system.transactions), 1)
        self.assertIn("Alice", self.booking_system.transactions[0])
        self.mock_gateway.process_payment.assert_called_with(150, "4123456789012345")

    def test_book_ticket_payment_failure(self):
        self.mock_gateway.process_payment.return_value = False

        with self.assertRaises(ValueError) as context:
            self.booking_system.book_ticket("LH123", "Bob", "4123456789012345", 200)
        self.assertEqual(str(context.exception), "Zahlung fehlgeschlagen")
        self.assertEqual(self.flight.get_available_seats(), 2)
        self.assertEqual(len(self.booking_system.transactions), 0)

    def test_book_ticket_invalid_flight(self):
        self.mock_gateway.process_payment.return_value = True
        with self.assertRaises(ValueError) as context:
            self.booking_system.book_ticket("LH999", "Charlie", "4123456789012345", 100)
        self.assertEqual(str(context.exception), "Flug nicht gefunden")

    def test_booking_flow_multiple_seats(self):
        self.mock_gateway.process_payment.return_value = True

        seat1 = self.booking_system.book_ticket(
            "LH123", "Alice", "4123456789012345", 100
        )
        seat2 = self.booking_system.book_ticket("LH123", "Bob", "4123456789012345", 120)

        self.assertEqual(seat1, 1)
        self.assertEqual(seat2, 2)
        self.assertEqual(self.flight.reservations, {1: "Alice", 2: "Bob"})
        self.assertEqual(self.flight.get_available_seats(), 0)
        self.assertEqual(len(self.booking_system.transactions), 2)
        self.assertIn("Alice", self.booking_system.transactions[0])
        self.assertIn("Bob", self.booking_system.transactions[1])

    def test_booking_when_no_seats_available(self):
        self.mock_gateway.process_payment.return_value = True

        self.booking_system.book_ticket("LH123", "Alice", "4123456789012345", 100)
        self.booking_system.book_ticket("LH123", "Bob", "4123456789012345", 120)
        with self.assertRaises(ValueError) as context:
            self.booking_system.book_ticket("LH123", "Charlie", "4123456789012345", 80)
        self.assertEqual(str(context.exception), "Keine Sitzplätze mehr verfügbar")
        self.assertEqual(self.mock_gateway.process_payment.call_count, 3)


class FlaskTaskTestCase(unittest.TestCase):

    def setUp(self):
        app.config["TESTING"] = True
        self.client = app.test_client()

        global tasks, task_id_counter
        tasks.clear()
        task_id_counter = 1

    def test_add_task_success(self):
        task_data = {"title": "Buy groceries"}
        response = self.client.post(
            "/tasks", data=json.dumps(task_data), content_type="application/json"
        )
        self.assertEqual(response.status_code, 201)
        response_data = json.loads(response.data)
        self.assertEqual(response_data["title"], "Buy groceries")
        self.assertEqual(response_data["id"], 1)
        self.assertFalse(response_data["done"])

    def test_add_task_missing_title(self):
        task_data = {}
        response = self.client.post(
            "/tasks", data=json.dumps(task_data), content_type="application/json"
        )
        self.assertEqual(response.status_code, 400)

    def test_get_all_tasks(self):
        self.client.post(
            "/tasks",
            data=json.dumps({"title": "Task 1"}),
            content_type="application/json",
        )
        self.client.post(
            "/tasks",
            data=json.dumps({"title": "Task 2"}),
            content_type="application/json",
        )

        response = self.client.get("/tasks")
        self.assertEqual(response.status_code, 200)
        response_data = json.loads(response.data)
        self.assertEqual(len(response_data), 2)
        self.assertEqual(response_data[0]["title"], "Task 1")
        self.assertEqual(response_data[1]["title"], "Task 2")

    def test_get_single_task_not_found(self):
        response = self.client.get("/tasks/999")
        self.assertEqual(response.status_code, 404)


if __name__ == "__main__":
    unittest.main()
