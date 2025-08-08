package projects.myapiary.models

data class AccountRegRequest(
    val Name: String,
    val Surname: String,
    val Phone: String,
    val Mail: String,
    val Pass: String
)
