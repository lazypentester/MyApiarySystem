package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class Apiary (
    @SerializedName("id")
    val id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("address")
    val Address: String,
    @SerializedName("connectionWire")
    val ConnectionWire: Boolean,
    @SerializedName("connectionWireless")
    val ConnectionWireless: Boolean,
    @SerializedName("countOfBeehives")
    val CountOfBeehives: Int,
    @SerializedName("notifications")
    val Notifications: Boolean
)