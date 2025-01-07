from flask import jsonify, request
from flask_restful import Resource
from questions import Question


class QuestionResource(Resource):
    def __init__(self, questions):
        self.questions = questions

    def get(self, question_id):
        question = next((q for q in self.questions if q.id == question_id), None)
        if question:
            return jsonify(question.to_dict())
        return {"message": "Question not found"}, 404

    def post(self):
        data = request.get_json()
        new_question = Question(
            level=data["level"],
            text=data["text"],
            correct_answer=data["correct_answer"],
        )
        self.questions.append(new_question)
        return jsonify(new_question), 201

    def delete(self, question_id):
        global questions
        questions = [q for q in self.questions if q.id != question_id]
        return {"message": "Question deleted"}, 204

    def put(self, question_id):
        data = request.get_json()
        question = next((q for q in questions if q.id == question_id), None)
        if question:
            question.text = data["text"]
            question.level = data["level"]
            question.correct_answer = data["correct_answer"]
            return jsonify(question)
        return {"message": "Question not found"}, 404


class RandomQuestionResource(Resource):
    def __init__(self, questions):
        self.questions = questions

    def get(self):
        import random

        question = random.choice(self.questions)
        return jsonify(question.to_dict())


class AllQuestionsResource(Resource):
    def __init__(self, questions):
        self.questions = questions

    def get(self):
        return jsonify([question.to_dict() for question in self.questions])


class SearchQuestionsResource(Resource):
    def __init__(self, questions):
        self.questions = questions

    def get(self, search_text):
        filtered_questions = [q for q in self.questions if search_text in q.text]
        return jsonify([question.to_dict() for question in filtered_questions])
