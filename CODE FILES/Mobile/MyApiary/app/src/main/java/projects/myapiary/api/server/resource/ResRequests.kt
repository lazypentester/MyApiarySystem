package projects.myapiary.api.server.resource

import com.google.gson.JsonObject
import projects.myapiary.models.*
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.*
import java.lang.reflect.Type

interface ResRequests {
    @POST("Users/register")
    fun registration(@Body body: AccountRegRequest): Call<AccountRegResponce>

    @POST("Users/confirmationAccountSendCode")
    fun confirmationAccountSendCode(@Body body: AccountConfirmation): Call<JsonObject>

    @POST("Users/confirmationAccountCheckCode")
    fun confirmationAccountCheckCode(@Body body: AccountConfirmationCode): Call<JsonObject>

    @POST("Sessions/createSession/{user_id}")
    fun createSessionAndDevice(@Path("user_id") user_id: Int, @Header("Authorization") auth_token: String, @Body body: CreateSession): Call<CreateSession>

    @Headers("Content-Type: application/json")
    @GET("Apiaries/getallbyuserid/{user_id}")
    fun getApiaries(@Path("user_id") user_id: Int, @Header("Authorization") auth: String): Call<List<Apiary>>

    @POST("Apiaries/add")
    fun addNewApiary(@Header("Authorization") auth_token: String, @Body body: ApiaryAdd): Call<JsonObject>

    @Headers("Content-Type:application/json; charset=UTF-8")
    @GET("Beehives/getallbyapiaryid/{apiaryid}")
    fun getBeehivesByApiaryId(@Path("apiaryid") apiaryid: Int, @Header("Authorization") auth: String): Call<List<Beehive>>

    @POST("Beehives/add")
    fun addNewBeehive(@Header("Authorization") auth_token: String, @Body body: BeehiveAdd): Call<JsonObject>

    @GET("Devices/getallbyuser/{user_id}")
    fun getDevices(@Path("user_id") user_id: Int, @Header("Authorization") auth: String): Call<List<Device>>

    @GET("Sessions/getallbydevice/{device_id}")
    fun getSessions(@Path("device_id") device_id: Int, @Header("Authorization") auth: String): Call<List<Session>>

    @GET("Users/getbyid/{user_id}")
    fun getUserById(@Path("user_id") user_id: Int, @Header("Authorization") auth: String): Call<UserSettings>

    @PUT("Users/edit")
    fun editProfile(@Header("Authorization") auth: String, @Body body: UserSettings): Call<JsonObject>

    @DELETE("Apiaries/delete/{id}")
    fun deleteApiaries(@Path("id") id: Int, @Header("Authorization") auth: String): Call<JsonObject>

    @DELETE("Beehives/delete/{id}")
    fun deleteBeehives(@Path("id") id: Int, @Header("Authorization") auth: String): Call<JsonObject>

    @GET("Sensors/GetSensorsByBeehive/{beehive_id}")
    fun GetSensorsByBeehive(@Path("beehive_id") beehive_id: Int, @Header("Authorization") auth: String): Call<List<SensorsGetResponce>>

    @GET("Beehives/getallsensors/{beehive_id}")
    fun getAllSensorsByBeehive(@Path("beehive_id") beehive_id: Int, @Header("Authorization") auth: String): Call<JsonObject>

    @PUT("Sensors/editbyuser")
    fun editSensorsByUser(@Header("Authorization") auth: String, @Body body: List<SensorsUpdateRequest>): Call<JsonObject>

    @PUT("Apiaries/edit/{id}")
    fun editApiary(@Path("id") id: Int, @Header("Authorization") auth: String, @Body body: ApiaryUpdate): Call<JsonObject>

    @PUT("Beehives/edit/{id}")
    fun editBeehive(@Path("id") id: Int, @Header("Authorization") auth: String, @Body body: BeehiveUpdate): Call<JsonObject>

    @GET("Notifications/getallbyapiary/{apiary_id}")
    fun GetNotificationByApiary(@Path("apiary_id") apiary_id: Int, @Header("Authorization") auth: String): Call<List<NotificationsByApiary>>

    @PUT("Notifications/setreaded/{notification_id}")
    fun readNotification(@Path("notification_id") notification_id: Int, @Header("Authorization") auth: String): Call<JsonObject>
//
///*    @GET("Users/getApiaries/{id}")
//    fun getApiaries(@Path("id") id: Int, @Header("Authorization") auth: String): Call<List<Apiaries>>
//
//    @POST("Apiaries/add")
//    fun addApiaries(@Header("Authorization") auth: String, @Body body: ApAdd): Call<ApResponse>
//
//    @GET("Apiaries/getbeehives/{id}")
//    fun getBeehives(@Path("id") id: Int, @Header("Authorization") auth: String): Call<List<BeehivesModel>>
//
//    @DELETE("Beehives/delete/{id}")
//    fun deleteBeehives(@Path("id") id: Int, @Header("Authorization") auth: String): Call<BeehivesModel>
//
//    @GET("Beehives/get_sensors/{id}")
//    fun getSensors(@Path("id") id: Int, @Header("Authorization") auth: String): Call<List<SensorModel>>
//
//    @POST("Beehives/add")
//    fun addBeehives(@Header("Authorization") auth: String, @Body body: AddBee): Call<BeeResponse>*/
}