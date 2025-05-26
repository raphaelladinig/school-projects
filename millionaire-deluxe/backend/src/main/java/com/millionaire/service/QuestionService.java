package com.millionaire.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.millionaire.model.Question;
import com.millionaire.repository.QuestionRepository;

import java.util.List;
import java.util.Optional;
import java.util.Random;

@Service
public class QuestionService {

    @Autowired
    private QuestionRepository questionRepository;

    public List<Question> getAllQuestions() {
        return questionRepository.findAll();
    }

    public Question getRandomQuestion() {
        List<Question> questions = questionRepository.findAll();
        if (questions.isEmpty()) {
            return null;
        }
        Random random = new Random();
        return questions.get(random.nextInt(questions.size()));
    }

    public List<Question> searchQuestions(String searchText) {
        return questionRepository.findAll().stream()
                .filter(question -> question.getText().contains(searchText))
                .toList();
    }

    public Question getQuestionById(int id) {
        Optional<Question> question = questionRepository.findById(id);
        return question.orElse(null);
    }
}
