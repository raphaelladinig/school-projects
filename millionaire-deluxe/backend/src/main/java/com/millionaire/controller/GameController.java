package com.millionaire.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import com.millionaire.service.QuestionService;
import com.millionaire.model.Question;

import java.util.List;

@RestController
@RequestMapping("/api/game")
public class GameController {

    @Autowired
    private QuestionService questionService;

    @GetMapping("/questions")
    public List<Question> getAllQuestions() {
        return questionService.getAllQuestions();
    }

    @GetMapping("/questions/random")
    public Question getRandomQuestion() {
        return questionService.getRandomQuestion();
    }

    @GetMapping("/questions/search/{searchText}")
    public List<Question> searchQuestions(@PathVariable String searchText) {
        return questionService.searchQuestions(searchText);
    }

    @GetMapping("/questions/{id}")
    public Question getQuestionById(@PathVariable int id) {
        return questionService.getQuestionById(id);
    }
}
