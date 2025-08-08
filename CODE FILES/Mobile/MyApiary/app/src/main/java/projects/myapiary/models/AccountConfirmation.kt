package projects.myapiary.models

data class AccountConfirmation(
    val Id: Int,
    val Mail: String,
    val Phone: String,
    val PhoneConfirmSelected: Boolean,
    val MailConfirmSelected: Boolean
)
