package projects.myapiary.models

import com.google.gson.annotations.SerializedName

data class Device(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("mac_address")
    val Mac_address: String,
    @SerializedName("fingerprint")
    val Fingerprint: String
)
