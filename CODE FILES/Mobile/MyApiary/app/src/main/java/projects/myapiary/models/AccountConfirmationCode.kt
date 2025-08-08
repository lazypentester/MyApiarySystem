package projects.myapiary.models

data class AccountConfirmationCode(
    val Id: Int,
    val Mail: String,
    val Phone: String,
    val PhoneConfirmed: Boolean,
    val MailConfirmed: Boolean,
    val Code: String
)
