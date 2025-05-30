package com.example.controller;

import com.example.dto.ReactionRequestTo;
import com.example.dto.ReactionResponseTo;
import com.example.service.ReactionService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/v1.0/reactions")
public class ReactionController {

    private final ReactionService reactionService;

    public ReactionController(ReactionService reactionService) {
        this.reactionService = reactionService;
    }

    @PostMapping
    public ResponseEntity<ReactionResponseTo> createReaction(@Valid @RequestBody ReactionRequestTo request) {
        ReactionResponseTo response = reactionService.createReaction(request);
        return new ResponseEntity<>(response, HttpStatus.CREATED);
    }

    @GetMapping("/{id}")
    public ResponseEntity<ReactionResponseTo> getReaction(@PathVariable Long id) {
        ReactionResponseTo response = reactionService.getReaction(id);
        return ResponseEntity.ok(response);
    }

    @GetMapping
    public ResponseEntity<List<ReactionResponseTo>> getAllReactions() {
        List<ReactionResponseTo> response = reactionService.getAllReactions();
        return ResponseEntity.ok(response);
    }

    @PutMapping
    public ResponseEntity<ReactionResponseTo> updateReaction(@Valid @RequestBody ReactionRequestTo request) {
        if (request.getId() == null) {
            throw new IllegalArgumentException("ID must be provided");
        }
        ReactionResponseTo response = reactionService.updateReaction(request);
        return ResponseEntity.ok(response);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteReaction(@PathVariable Long id) {
        reactionService.deleteReaction(id);
        return ResponseEntity.noContent().build();
    }
}
