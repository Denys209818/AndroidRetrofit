package com.example.registeruserwithaspnetserver.network;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitService {
    private String baseUrl = "http://10.0.2.2:5054";
    private static  RetrofitService retrofitService;
    private Retrofit retrofit;

    private RetrofitService()
    {
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .writeTimeout(20, TimeUnit.SECONDS)
                .readTimeout(20, TimeUnit.SECONDS)
                .callTimeout(30, TimeUnit.SECONDS)
                .build();

        retrofit = new Retrofit.Builder()
                .client(okHttpClient)
                .baseUrl(baseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }

    public RetrofitAccountApi getRetrofitService()
    {
        return retrofit.create(RetrofitAccountApi.class);
    }

    public static RetrofitService getInstance()
    {
        if(retrofitService == null)
            retrofitService = new RetrofitService();

        return retrofitService;
    }
}
