package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class Session(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("start_date")
    val Start_date: String,
    @SerializedName("ip_address")
    val Ip_address: String,
    @SerializedName("geolocation")
    val Geolocation: String,
    @SerializedName("deviceId")
    val DeviceId: Int,
)
