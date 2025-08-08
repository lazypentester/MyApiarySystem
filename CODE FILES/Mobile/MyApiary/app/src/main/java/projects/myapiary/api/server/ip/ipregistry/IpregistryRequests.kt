package projects.myapiary.api.server.ip.ipregistry

import com.google.gson.JsonObject
import retrofit2.Call
import retrofit2.http.GET

interface IpregistryRequests {
    @GET("?key=uhx064yrbubhp9pc")
    fun getClientIpInfoJsonOriginIp(): Call<JsonObject>
}