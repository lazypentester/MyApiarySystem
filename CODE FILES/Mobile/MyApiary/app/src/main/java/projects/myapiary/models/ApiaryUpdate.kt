package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class ApiaryUpdate(
    @SerializedName("Id")
    val id: Int,
    @SerializedName("Name")
    val Name: String,
    @SerializedName("Address")
    val Address: String,
    @SerializedName("UserId")
    val userId: Int,
)
