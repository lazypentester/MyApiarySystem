package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class Beehive(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("alarm")
    var Alarm: Boolean,
    @SerializedName("sensorTemperature")
    var SensorTemperature: Float,
    @SerializedName("sensorHumidity")
    val SensorHumidity: Float,
    @SerializedName("sensorNoise")
    val SensorNoise: Float
)
