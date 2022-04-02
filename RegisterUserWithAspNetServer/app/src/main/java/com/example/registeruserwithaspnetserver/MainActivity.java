package com.example.registeruserwithaspnetserver;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.ContextCompat;

import android.content.res.Resources;
import android.graphics.Color;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.example.registeruserwithaspnetserver.network.RetrofitService;
import com.example.registeruserwithaspnetserver.network.dto.RegisterUserDto;
import com.example.registeruserwithaspnetserver.network.dto.ReturnRegisterUserDto;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private String jwtToken;
    private EditText txtName;
    private EditText txtSurname;
    private EditText txtEmail;
    private EditText txtPhone;
    private EditText txtPassword;
    private EditText txtConfirmPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        this.txtName = findViewById(R.id.txtName);
        this.txtSurname = findViewById(R.id.txtSurname);
        this.txtEmail = findViewById(R.id.txtEmail);
        this.txtPhone = findViewById(R.id.txtPhone);
        this.txtPassword = findViewById(R.id.txtPassword);
        this.txtConfirmPassword = findViewById(R.id.txtConfirmPassword);
        setBlackUnderline();
    }

    public void onSubmitHander(View view)
    {
        if(!isEmpty()) {
        RegisterUserDto registerUserDto = new RegisterUserDto();

        registerUserDto.setEmail(this.txtEmail.getText().toString());
        registerUserDto.setFirstName(this.txtName.getText().toString());
        registerUserDto.setSecondName(this.txtSurname.getText().toString());
        registerUserDto.setPhone(this.txtPhone.getText().toString());
        registerUserDto.setPassword(this.txtPassword.getText().toString());
        registerUserDto.setConfirmPassword(this.txtConfirmPassword.getText().toString());

        RetrofitService.getInstance()
                .getRetrofitService()
                .register(registerUserDto)
        .enqueue(new Callback<ReturnRegisterUserDto>() {
            @Override
            public void onResponse(Call<ReturnRegisterUserDto> call, Response<ReturnRegisterUserDto> response) {
                jwtToken = response.body().getToken();

                Toast.makeText(getApplicationContext(), "Успішно!", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onFailure(Call<ReturnRegisterUserDto> call, Throwable t) {

                String error = t.getMessage();
                Toast.makeText(getApplicationContext(),
                        error, Toast.LENGTH_LONG).show();
            }
        });
            clearEditText();
        }
    }

    public Boolean isEmpty()
    {
        setBlackUnderline();
        if(this.txtName.getText().toString().isEmpty())
        {
            setBackgroundForEditText(this.txtName, R.color.red);
            return true;
        }

        if(this.txtSurname.getText().toString().isEmpty())
        {
            setBackgroundForEditText(this.txtSurname, R.color.red);
            return true;
        }

        if(this.txtEmail.getText().toString().isEmpty())
        {
            setBackgroundForEditText(this.txtEmail, R.color.red);
            return true;
        }

        if(this.txtPhone.getText().toString().isEmpty())
        {
            setBackgroundForEditText(this.txtPhone, R.color.red);
            return true;
        }

        if(this.txtPassword.getText().toString().isEmpty() || this.txtPassword.getText().length() < 6)
        {

            setBackgroundForEditText(this.txtPassword, R.color.red);
            return true;
        }

        if(this.txtConfirmPassword.getText().toString().isEmpty()||
                this.txtConfirmPassword.getText().length() < 6
        ||
                this.txtPassword.getText().toString()
                        .compareTo(this.txtConfirmPassword.getText().toString()) != 0
                        )
        {
            setBackgroundForEditText(this.txtConfirmPassword, R.color.red);
            return true;
        }
        return false;
    }

    public void setBlackUnderline()
    {
        setBackgroundForEditText(this.txtName, R.color.black);
        setBackgroundForEditText(this.txtSurname, R.color.black);
        setBackgroundForEditText(this.txtPhone, R.color.black);
        setBackgroundForEditText(this.txtEmail, R.color.black);
        setBackgroundForEditText(this.txtPassword, R.color.black);
        setBackgroundForEditText(this.txtConfirmPassword, R.color.black);
    }

    public void setBackgroundForEditText(EditText editText, int Color)
    {
        editText.setBackgroundTintList(getApplicationContext().getResources()
                .getColorStateList(Color, null));
    }

    public void clearEditText()
    {
        this.txtName.setText("");
        this.txtSurname.setText("");
        this.txtEmail.setText("");
        this.txtPhone.setText("");
        this.txtPassword.setText("");
        this.txtConfirmPassword.setText("");
    }
}