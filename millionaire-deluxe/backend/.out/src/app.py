import os
from flask import Flask, render_template_string, session, render_template
from flask_restful import Api
from flask_cors import CORS
from db import Base, engine
from questions import (
    get_questions,
    get_rand_question,
    read_questions,
    QuestionResource,
    RandomQuestionResource,
    AllQuestionsResource,
    SearchQuestionsResource,
)


app = Flask(__name__)
CORS(app)
app.secret_key = "secret"
api = Api(app)

if not os.path.exists("questions.db"):
    Base.metadata.create_all(engine)
    print("Creating Database...")
    read_questions("millionaire.txt")


questions = get_questions()


@app.route("/")
def main():
    return """
    <a href="/game">Start Game</a>
   <p/> 
   <a href="/search">Search</a>
"""


@app.route("/search")
def search():
    return render_template("questions.html", questions=questions)


@app.route("/game")
@app.route("/game/<int:answer_index>")
def game(answer_index=None):
    current_level = session.get("current_level", 0)
    correct_index = session.get("correct_index", 0)

    if answer_index is None:
        current_level = 0
        correct_index = -1

    if correct_index != -1:
        print(f"Answer Index: {answer_index}, Correct Index: {correct_index}")
        print(f"Answer Index: {answer_index} (type: {type(answer_index)})")
        print(f"Correct Index: {correct_index} (type: {type(correct_index)})")

        if answer_index == correct_index:
            current_level += 1
            session["current_level"] = current_level
            session["correct_index"] = -1
            print("correct")

            if current_level >= 4:
                current_level = 0
                session["current_level"] = current_level
                return """
                    You are a Millionaire
                    <p/>
                    <a href="/game">Restart</a>
                    <p/>
                    <a href="/">Go back</a>
                """
        else:
            print("incorrect")
            current_level = 0
            session["current_level"] = current_level
            return """
                Game Over 
                <p/>
                <a href="/game">Restart</a>
                <p/>
                <a href="/">Go back</a>
            """

    question = get_rand_question(current_level)
    if question is None:
        return """
            No more questions available for this level.
            <p/>
            <a href="/game">Restart</a>
            <p/>
            <a href="/">Go back</a>
        """
    session["correct_index"] = question.correct_index

    return render_template_string(
        """
        <p>Your current level is {{ current_level }}</p>
        <p>{{ question.text }}</p>
            {% for i, answer in enumerate(question.answers) %}
                <a href="/game/{{ i }}" role="button">{{ answer }}<a/>
                <br/>
            {% endfor %}
        <p/>
        <a href="/">Go back</a>
    """,
        current_level=current_level,
        question=question,
        enumerate=enumerate,
    )


api.add_resource(
    QuestionResource,
    "/api/questions/<int:question_id>",
    resource_class_kwargs={"questions": questions},
)
api.add_resource(
    RandomQuestionResource,
    "/api/questions/random",
    resource_class_kwargs={"questions": questions},
)
api.add_resource(
    AllQuestionsResource,
    "/api/questions",
    resource_class_kwargs={"questions": questions},
)
api.add_resource(
    SearchQuestionsResource,
    "/api/questions/search/<string:search_text>",
    resource_class_kwargs={"questions": questions},
)


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
