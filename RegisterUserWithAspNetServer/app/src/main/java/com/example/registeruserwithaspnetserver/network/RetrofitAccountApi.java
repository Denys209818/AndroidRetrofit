package com.example.registeruserwithaspnetserver.network;

import com.example.registeruserwithaspnetserver.network.dto.RegisterUserDto;
import com.example.registeruserwithaspnetserver.network.dto.ReturnRegisterUserDto;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface RetrofitAccountApi {
    @POST("/api/account/register")
    public Call<ReturnRegisterUserDto> register(@Body RegisterUserDto user);
}
