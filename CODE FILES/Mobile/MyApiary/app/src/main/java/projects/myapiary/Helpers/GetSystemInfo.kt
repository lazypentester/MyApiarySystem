package projects.myapiary.Helpers


import android.os.Build
import android.text.TextUtils


class GetSystemInfo {
    companion object {
        fun parseStringLocale(): String? {
            val manufacturer = Build.MANUFACTURER
            val model = Build.MODEL
            if (model.startsWith(manufacturer)) return capitalize(model)

            return capitalize(manufacturer).toString() + " " + model
        }

        private fun capitalize(str: String): String? {
            if (TextUtils.isEmpty(str)) {
                return str
            }
            val arr = str.toCharArray()
            var capitalizeNext = true
            var phrase: String? = ""
            for (c in arr) {
                if (capitalizeNext && Character.isLetter(c)) {
                    phrase += Character.toUpperCase(c)
                    capitalizeNext = false
                    continue
                } else if (Character.isWhitespace(c)) {
                    capitalizeNext = true
                }
                phrase += c
            }
            return phrase
        }

    }
}