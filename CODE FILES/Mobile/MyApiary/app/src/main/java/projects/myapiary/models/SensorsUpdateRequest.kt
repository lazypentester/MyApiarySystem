package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class SensorsUpdateRequest(
    @SerializedName("sensor_id")
    val id: Int,
    @SerializedName("minValue")
    val minvalue: Float,
    @SerializedName("maxValue")
    val maxvalue: Float,
    @SerializedName("isWorking")
    val isworking: Boolean?,
    @SerializedName("beehiveId")
    val beehiveId: Int
)
