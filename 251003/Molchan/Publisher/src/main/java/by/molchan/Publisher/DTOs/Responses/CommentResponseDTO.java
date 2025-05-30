package by.molchan.Publisher.DTOs.Responses;

import java.io.Serializable;

public class CommentResponseDTO implements Serializable {
    private long id;
    private Long articleId;

    private String content;

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public Long getArticleId() {
        return articleId;
    }

    public void setArticleId(Long articleId) {
        this.articleId = articleId;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
}
