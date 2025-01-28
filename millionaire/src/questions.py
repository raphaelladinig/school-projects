import random
from sqlalchemy import Column, Integer, String, Sequence
from db import Base, Session
from flask import jsonify, request
from flask_restful import Resource


class QuestionModel(Base):
    __tablename__ = "questions"

    id = Column(Integer, Sequence("question_id_seq"), primary_key=True)
    text = Column(String)
    level = Column(Integer)
    correct_index = Column(Integer)
    answers = Column(String)

    def __init__(self, text, level, correct_index, answers):
        self.text = text
        self.level = level
        self.correct_index = correct_index
        self.answers = answers

    def to_question(self):
        answers_list = self.answers.split(",")
        return Question(
            self.id, self.text, self.level, self.correct_index, *answers_list
        )


class Question:
    def __init__(self, id, text, level, correct_index, *answers):
        self.id = id
        self.text = text
        self.level = level
        self.answers = answers
        self.correct_index = correct_index

    def __str__(self):
        answers_str = "\n".join(
            f"{answer} ({index})" for index, answer in enumerate(self.answers)
        )
        return f"{self.text}\n{answers_str}\ncorrect_index: {self.correct_index}"

    def to_dict(self):
        return {
            "id": self.id,
            "text": self.text,
            "level": self.level,
            "answers": self.answers,
            "correct_index": self.correct_index,
        }


def read_questions(fName):
    with open(fName, "r") as file:
        for line in file:
            parts = line.strip().split("\t")
            level = int(parts[0])
            text = parts[1]
            correct_answer = parts[2]
            options = parts[3:6]
            save_question_to_db(level, text, correct_answer, *options)


def save_question_to_db(level, text, correct_answer, *options):
    session = Session()
    answers = list(options) + [correct_answer]
    random.shuffle(answers)
    correct_index = answers.index(correct_answer)
    answers_str = ",".join(answers)
    question_model = QuestionModel(
        text=text, level=level, correct_index=correct_index, answers=answers_str
    )
    session.add(question_model)
    session.commit()
    session.close()


def get_rand_question(level):
    session = Session()
    level_questions = session.query(QuestionModel).filter_by(level=level).all()
    session.close()
    if level_questions:
        selected_question = random.choice(level_questions)
        return selected_question.to_question()
    return None


def get_questions():
    session = Session()
    question_models = session.query(QuestionModel).all()
    session.close()

    if question_models:
        return [question_model.to_question() for question_model in question_models]

    return []


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
            id=data["id"],
            level=data["level"],
            text=data["text"],
            correct_index=data["correct_answer"],
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
