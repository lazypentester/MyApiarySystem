package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class BeehiveUpdate(
    @SerializedName("Id")
    val id: Int,
    @SerializedName("Name")
    val Name: String,
    @SerializedName("Alarm")
    val Alarm: Boolean,
    @SerializedName("ApiariId")
    val ApiariId: Int,
)
