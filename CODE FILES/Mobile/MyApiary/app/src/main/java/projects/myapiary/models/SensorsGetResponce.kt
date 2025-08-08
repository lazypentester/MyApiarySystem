package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class SensorsGetResponce(
    @SerializedName("id")
    val id: Int,
    @SerializedName("minValue")
    val minvalue: Float,
    @SerializedName("maxValue")
    val maxvalue: Float,
    @SerializedName("isWorking")
    val isworking: Boolean,
    @SerializedName("type")
    val type: String,
    @SerializedName("serialNumber")
    val serialnumber: String,
    @SerializedName("beehiveId")
    val beehiveId: Int,
    @SerializedName("baseStationName")
    val basestationname: String,
    @SerializedName("baseStationSerialNumber")
    val basestationserialnumber: String
)
