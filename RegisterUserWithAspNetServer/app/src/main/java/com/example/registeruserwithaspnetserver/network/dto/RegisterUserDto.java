package com.example.registeruserwithaspnetserver.network.dto;

import lombok.Data;

@Data
public class RegisterUserDto {
    private String FirstName;
    private String SecondName;
    private String Email;
    private String Phone;
    private String Password;
    private String ConfirmPassword;
}
