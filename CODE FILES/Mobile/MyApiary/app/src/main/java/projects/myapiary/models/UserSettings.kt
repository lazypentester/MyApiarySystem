package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class UserSettings(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("surname")
    val Surname: String,
    @SerializedName("phone")
    val Phone: String,
    @SerializedName("mail")
    val Mail: String,
    @SerializedName("tariff")
    val Tariff: String
)
