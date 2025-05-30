package com.lab.labDCP.dto;

import lombok.*;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class UserRequestTo {
    private Long id;
    private String login;
    private String password;
    private String firstname;
    private String lastname;
}
