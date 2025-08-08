package projects.myapiary.models

data class CreateSession(
    val Device_id: Int,
    val Device_Name: String,
    val Device_Mac_address: String,
    val Device_Fingerprint: String,
    val Device_Refresh_token: String,
    val Session_Id: Int,
    val Session_Ip_address: String,
    val Session_Geolocation: String
)
