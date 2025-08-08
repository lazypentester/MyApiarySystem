package projects.myapiary.api.server.ip.api64_ipify_org

import com.google.gson.JsonObject
import projects.myapiary.models.Device
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Path

interface IpifyRequests {

    @GET("?format=json")
    fun getPublicIpJson(): Call<JsonObject>

    @GET("?format=string")
    fun getPublicIpString(): Call<String>

    //@GET("Value/value/{value1}")
    //fun getDevices(@Path("value1") value1: String, @Header("Authorization") auth: String): Call<JsonObject>
}