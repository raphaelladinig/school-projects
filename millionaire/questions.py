import random


class Question:
    _id_counter = 0

    def __init__(self, text, level, correct_answer, *options):
        self.id = Question._id_counter
        Question._id_counter += 1

        self.text = text
        self.level = level
        self.answers = list(options) + [correct_answer]
        random.shuffle(self.answers)
        self.correct_index = self.answers.index(correct_answer)

    def __str__(self):
        options_str = "\n".join(
            f"{option} ({index})" for index, option in enumerate(self.answers)
        )
        return f"{self.text}\n{options_str}"

    def to_dict(self):
        return {
            "id": self.id,
            "text": self.text,
            "level": self.level,
            "answers": self.answers,
            "correct_index": self.correct_index,
        }


def read_questions(fName):
    questions = []
    with open(fName, "r") as file:
        file.readline()
        for line in file:
            parts = line.strip().split("\t")
            level = int(parts[0])
            question_text = parts[1]
            correct_answer = parts[2]
            options = parts[3:7]
            question = Question(question_text, level, correct_answer, *options)
            questions.append(question)
    return questions


def get_rand_question(level, questions):
    level_questions = [q for q in questions if q.level == level]
    return random.choice(level_questions) if level_questions else None
