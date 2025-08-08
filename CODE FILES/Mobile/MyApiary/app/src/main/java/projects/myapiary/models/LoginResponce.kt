package projects.myapiary.models

data class LoginResponce(
    val access_token_short: String,
    val access_token_long: String,
    val user_id: Int,
    val name: String,
    val surname: String,
    val phone: String,
    val mail: String,
    val tariffId: Int,
    val tariff_name: String,
    val max_apiaries: Int,
    val max_beehives: Int,
    val price: Double,
    val confirmed: Boolean
)
