package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class NotificationsByApiary(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("description")
    val Description: String,
    @SerializedName("created_date")
    val Created_date: String,
    @SerializedName("readed")
    val Readed: Boolean,
    @SerializedName("apiaryId")
    val ApiaryId: Int,
)
