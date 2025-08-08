package projects.myapiary.api.server.auth

import projects.myapiary.models.LoginRequest
import projects.myapiary.models.LoginResponce
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST

interface AuthRequests {
    @POST("auth/login")
    fun login(@Body body: LoginRequest): Call<LoginResponce>
}