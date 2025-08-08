package projects.myapiary

import android.annotation.SuppressLint
import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.*
import com.google.gson.JsonObject
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.SensorsGetResponce
import projects.myapiary.models.SensorsUpdateRequest
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.lang.Exception
import java.text.DecimalFormat
import java.util.*

class ActivitySingleBeehiveScreen : AppCompatActivity() {

    private var buttonGoBackFromBeehiveSingle: Button? = null
    private var buttonCancelUpdateBeehiveSensors: Button? = null
    private var buttonUpdateSensorsByUser: Button? = null

    @SuppressLint("UseSwitchCompatOrMaterialCode")
    private var switchTemperature: Switch? = null
    @SuppressLint("UseSwitchCompatOrMaterialCode")
    private var switchNoise: Switch? = null
    @SuppressLint("UseSwitchCompatOrMaterialCode")
    private var switchHumidity: Switch? = null

    private var linearLayoutTemperature: LinearLayout? = null
    private var linearLayoutNoise: LinearLayout? = null
    private var linearLayoutHumidity: LinearLayout? = null

    private var imageViewWorkingTemperature: ImageView? = null
    private var imageWorkingNoise: ImageView? = null
    private var imageWorkingHumidity: ImageView? = null

    private var sensorTemperatureMinValue: EditText? = null
    private var sensorTemperatureMaxValue: EditText? = null
    private var sensorNoiseMinValue: EditText? = null
    private var sensorNoiseMaxValue: EditText? = null
    private var sensorHumidityMinValue: EditText? = null
    private var sensorHumidityMaxValue: EditText? = null

    private var buttonTemperatureMinUp: Button? = null
    private var buttonTemperatureMinDown: Button? = null
    private var buttonTemperatureMaxUp: Button? = null
    private var buttonTemperatureMaxDown: Button? = null
    private var buttonNoiseMinUp: Button? = null
    private var buttonNoiseMinDown: Button? = null
    private var buttonNoiseMaxUp: Button? = null
    private var buttonNoiseMaxDown: Button? = null
    private var buttonHumidityMinUp: Button? = null
    private var buttonHumidityMinDown: Button? = null
    private var buttonHumidityMaxUp: Button? = null
    private var buttonHumidityMaxDown: Button? = null

    private var textBaseStationName: TextView? = null
    private var textBaseStationSerialNum: TextView? = null

    private var formatStrSensorValue = "#0.0"



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_single_beehive_screen)

        var context = this

        buttonGoBackFromBeehiveSingle = findViewById(R.id.buttonGoBackFromBeehiveSingle)
        buttonCancelUpdateBeehiveSensors = findViewById(R.id.buttonCancelUpdateBeehiveSensors)
        buttonUpdateSensorsByUser = findViewById(R.id.buttonUpdateSensorsByUser)


        buttonGoBackFromBeehiveSingle?.setOnClickListener {
            onBackPressed()
        }

        buttonCancelUpdateBeehiveSensors?.setOnClickListener {
            onBackPressed()
        }

        buttonUpdateSensorsByUser?.setOnClickListener {


            if(sensorTemperatureMinValue?.text.toString() == "" ||
                sensorTemperatureMaxValue?.text.toString() == "" ||
                sensorNoiseMinValue?.text.toString() == "" ||
                sensorNoiseMaxValue?.text.toString() == "" ||
                sensorHumidityMinValue?.text.toString() == "" ||
                sensorHumidityMaxValue?.text.toString() == "") {

                Toast.makeText(this, "Одне з полів не заповнено", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            val numTMaxV = sensorTemperatureMinValue?.text.toString().toFloat()
            val numTMinV = sensorTemperatureMaxValue?.text.toString().toFloat()
            val numNMaxV = sensorNoiseMinValue?.text.toString().toFloat()
            val numNMinV = sensorNoiseMaxValue?.text.toString().toFloat()
            val numHMaxV = sensorHumidityMinValue?.text.toString().toFloat()
            val numHMinV = sensorHumidityMaxValue?.text.toString().toFloat()



            /// REQUEST FOR GET USER

            val list: List<SensorsUpdateRequest> = arrayListOf(
                SensorsUpdateRequest(Storage.Selected_t_sensor_id, numTMaxV, numTMinV,switchTemperature?.isChecked, Storage.Selected_beehive_id),
                SensorsUpdateRequest(Storage.Selected_n_sensor_id, numNMaxV, numNMinV,switchNoise?.isChecked, Storage.Selected_beehive_id),
                SensorsUpdateRequest(Storage.Selected_h_sensor_id, numHMaxV, numHMinV,switchHumidity?.isChecked, Storage.Selected_beehive_id)
            )

            val requests = ResClientBuilder.buildClient(ResRequests::class.java)
            val response = requests.editSensorsByUser("Bearer ${Storage.token_short}", list)

            response.enqueue(object: retrofit2.Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {

                        val data = response.body()!!
                        Toast.makeText(context, "Данні сенсорів збережено", Toast.LENGTH_SHORT).show()
                        onBackPressed()

                    } else {
                        Toast.makeText(context, "Помилка при збереженні данних сенсорів", Toast.LENGTH_SHORT).show()
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Toast.makeText(context, "Помилка при збереженні данних сенсорів", Toast.LENGTH_SHORT).show()
                }
            })


            /// REQUEST FOR GET USER END


        }

        textBaseStationName = findViewById(R.id.textBaseStationName)
        textBaseStationSerialNum = findViewById(R.id.textBaseStationSerialNum)

        linearLayoutTemperature = findViewById(R.id.linearLayoutTemperature)
        linearLayoutNoise = findViewById(R.id.linearLayoutNoise)
        linearLayoutHumidity = findViewById(R.id.linearLayoutHumidity)

        switchTemperature = findViewById(R.id.switchTemperature)
        switchNoise = findViewById(R.id.switchNoise)
        switchHumidity = findViewById(R.id.switchHumidity)

        imageViewWorkingTemperature = findViewById(R.id.imageViewWorkingTemperature)
        imageWorkingNoise = findViewById(R.id.imageWorkingNoise)
        imageWorkingHumidity = findViewById(R.id.imageWorkingHumidity)

        sensorTemperatureMinValue = findViewById(R.id.sensorTemperatureMinValue)
        sensorTemperatureMaxValue = findViewById(R.id.sensorTemperatureMaxValue)
        sensorNoiseMinValue = findViewById(R.id.sensorNoiseMinValue)
        sensorNoiseMaxValue = findViewById(R.id.sensorNoiseMaxValue)
        sensorHumidityMinValue = findViewById(R.id.sensorHumidityMinValue)
        sensorHumidityMaxValue = findViewById(R.id.sensorHumidityMaxValue)

        buttonTemperatureMinUp = findViewById(R.id.buttonTemperatureMinUp)
        buttonTemperatureMinDown = findViewById(R.id.buttonTemperatureMinDown)
        buttonTemperatureMaxUp = findViewById(R.id.buttonTemperatureMaxUp)
        buttonTemperatureMaxDown = findViewById(R.id.buttonTemperatureMaxDown)
        buttonNoiseMinUp = findViewById(R.id.buttonNoiseMinUp)
        buttonNoiseMinDown = findViewById(R.id.buttonNoiseMinDown)
        buttonNoiseMaxUp = findViewById(R.id.buttonNoiseMaxUp)
        buttonNoiseMaxDown = findViewById(R.id.buttonNoiseMaxDown)
        buttonHumidityMinUp = findViewById(R.id.buttonHumidityMinUp)
        buttonHumidityMinDown = findViewById(R.id.buttonHumidityMinDown)
        buttonHumidityMaxUp = findViewById(R.id.buttonHumidityMaxUp)
        buttonHumidityMaxDown = findViewById(R.id.buttonHumidityMaxDown)

        switchTemperature?.setOnClickListener {
            if (switchTemperature?.isChecked == true) {
                Toast.makeText(this, "Датчик температури - ON", Toast.LENGTH_SHORT).show()
                imageViewWorkingTemperature?.setImageResource(R.drawable.check_mark_svg_1)
                sensorTemperatureMinValue?.isEnabled = true
                sensorTemperatureMaxValue?.isEnabled = true
                buttonTemperatureMinUp?.isEnabled = true
                buttonTemperatureMinDown?.isEnabled = true
                buttonTemperatureMaxUp?.isEnabled = true
                buttonTemperatureMaxDown?.isEnabled = true
                linearLayoutTemperature?.setBackgroundColor(Color.parseColor("#FFFFFF"))
            }
            else {
                Toast.makeText(this, "Датчик температури - OFF", Toast.LENGTH_SHORT).show()
                imageViewWorkingTemperature?.setImageResource(R.drawable.close_square_svg)
                sensorTemperatureMinValue?.isEnabled = false
                sensorTemperatureMaxValue?.isEnabled = false
                buttonTemperatureMinUp?.isEnabled = false
                buttonTemperatureMinDown?.isEnabled = false
                buttonTemperatureMaxUp?.isEnabled = false
                buttonTemperatureMaxDown?.isEnabled = false
                linearLayoutTemperature?.setBackgroundColor(Color.parseColor("#E8E8E8"))
            }
        }

        switchNoise?.setOnClickListener {
            if (switchNoise?.isChecked == true) {
                Toast.makeText(this, "Датчик шуму - ON", Toast.LENGTH_SHORT).show()
                imageWorkingNoise?.setImageResource(R.drawable.check_mark_svg_1)
                sensorNoiseMinValue?.isEnabled = true
                sensorNoiseMaxValue?.isEnabled = true
                buttonNoiseMinUp?.isEnabled = true
                buttonNoiseMinDown?.isEnabled = true
                buttonNoiseMaxUp?.isEnabled = true
                buttonNoiseMaxDown?.isEnabled = true
                linearLayoutNoise?.setBackgroundColor(Color.parseColor("#FFFFFF"))
            }
            else {
                Toast.makeText(this, "Датчик шуму - OFF", Toast.LENGTH_SHORT).show()
                imageWorkingNoise?.setImageResource(R.drawable.close_square_svg)
                sensorNoiseMinValue?.isEnabled = false
                sensorNoiseMaxValue?.isEnabled = false
                buttonNoiseMinUp?.isEnabled = false
                buttonNoiseMinDown?.isEnabled = false
                buttonNoiseMaxUp?.isEnabled = false
                buttonNoiseMaxDown?.isEnabled = false
                linearLayoutNoise?.setBackgroundColor(Color.parseColor("#E8E8E8"))
            }
        }

        switchHumidity?.setOnClickListener {
            if (switchHumidity?.isChecked == true) {
                Toast.makeText(this, "Датчик вологості - ON", Toast.LENGTH_SHORT).show()
                imageWorkingHumidity?.setImageResource(R.drawable.check_mark_svg_1)
                sensorHumidityMinValue?.isEnabled = true
                sensorHumidityMaxValue?.isEnabled = true
                buttonHumidityMinUp?.isEnabled = true
                buttonHumidityMinDown?.isEnabled = true
                buttonHumidityMaxUp?.isEnabled = true
                buttonHumidityMaxDown?.isEnabled = true
                linearLayoutHumidity?.setBackgroundColor(Color.parseColor("#FFFFFF"))
            }
            else {
                Toast.makeText(this, "Датчик вологості - OFF", Toast.LENGTH_SHORT).show()
                imageWorkingHumidity?.setImageResource(R.drawable.close_square_svg)
                sensorHumidityMinValue?.isEnabled = false
                sensorHumidityMaxValue?.isEnabled = false
                buttonHumidityMinUp?.isEnabled = false
                buttonHumidityMinDown?.isEnabled = false
                buttonHumidityMaxUp?.isEnabled = false
                buttonHumidityMaxDown?.isEnabled = false
                linearLayoutHumidity?.setBackgroundColor(Color.parseColor("#E8E8E8"))
            }
        }

        buttonTemperatureMinUp?.setOnClickListener {
            sensorTemperatureMinValue?.setText(numPlusAndConvertToStr(sensorTemperatureMinValue?.text.toString(), 0.1))
        }
        buttonTemperatureMinDown?.setOnClickListener {
            sensorTemperatureMinValue?.setText(numMinusAndConvertToStr(sensorTemperatureMinValue?.text.toString(), 0.1))
        }
        buttonTemperatureMaxUp?.setOnClickListener {
            sensorTemperatureMaxValue?.setText(numPlusAndConvertToStr(sensorTemperatureMaxValue?.text.toString(), 0.1))
        }
        buttonTemperatureMaxDown?.setOnClickListener {
            sensorTemperatureMaxValue?.setText(numMinusAndConvertToStr(sensorTemperatureMaxValue?.text.toString(), 0.1))
        }


        buttonNoiseMinUp?.setOnClickListener {
            sensorNoiseMinValue?.setText(numPlusAndConvertToStr(sensorNoiseMinValue?.text.toString(), 0.1))
        }
        buttonNoiseMinDown?.setOnClickListener {
            sensorNoiseMinValue?.setText(numMinusAndConvertToStr(sensorNoiseMinValue?.text.toString(), 0.1))
        }
        buttonNoiseMaxUp?.setOnClickListener {
            sensorNoiseMaxValue?.setText(numPlusAndConvertToStr(sensorNoiseMaxValue?.text.toString(), 0.1))
        }
        buttonNoiseMaxDown?.setOnClickListener {
            sensorNoiseMaxValue?.setText(numMinusAndConvertToStr(sensorNoiseMaxValue?.text.toString(), 0.1))
        }


        buttonHumidityMinUp?.setOnClickListener {
            sensorHumidityMinValue?.setText(numPlusAndConvertToStr(sensorHumidityMinValue?.text.toString(), 0.1))
        }
        buttonHumidityMinDown?.setOnClickListener {
            sensorHumidityMinValue?.setText(numMinusAndConvertToStr(sensorHumidityMinValue?.text.toString(), 0.1))
        }
        buttonHumidityMaxUp?.setOnClickListener {
            sensorHumidityMaxValue?.setText(numPlusAndConvertToStr(sensorHumidityMaxValue?.text.toString(), 0.1))
        }
        buttonHumidityMaxDown?.setOnClickListener {
            sensorHumidityMaxValue?.setText(numMinusAndConvertToStr(sensorHumidityMaxValue?.text.toString(), 0.1))
        }

        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.GetSensorsByBeehive(Storage.Selected_beehive_id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<SensorsGetResponce>> {
            override fun onResponse(call: Call<List<SensorsGetResponce>>, response: Response<List<SensorsGetResponce>>) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    data.forEach {
                        when(it.type.lowercase()) {
                            "sensor_temperature" -> {
                                Storage.Selected_t_sensor_id = it.id
                                sensorTemperatureMinValue?.setText(String.format(Locale.ROOT, "%.1f", it.minvalue))
                                sensorTemperatureMaxValue?.setText(String.format(Locale.ROOT, "%.1f", it.maxvalue))
                                if(it.isworking) {
                                    sensorTemperatureMinValue?.isEnabled = true
                                    sensorTemperatureMaxValue?.isEnabled = true
                                    imageViewWorkingTemperature?.setImageResource(R.drawable.check_mark_svg_1)
                                    switchTemperature?.isChecked = true
                                    buttonTemperatureMinUp?.isEnabled = true
                                    buttonTemperatureMinDown?.isEnabled = true
                                    buttonTemperatureMaxUp?.isEnabled = true
                                    buttonTemperatureMaxDown?.isEnabled = true
                                    linearLayoutTemperature?.setBackgroundColor(Color.parseColor("#FFFFFF"))
                                } else {
                                    sensorTemperatureMinValue?.isEnabled = false
                                    sensorTemperatureMaxValue?.isEnabled = false
                                    imageViewWorkingTemperature?.setImageResource(R.drawable.close_square_svg)
                                    switchTemperature?.isChecked = false
                                    buttonTemperatureMinUp?.isEnabled = false
                                    buttonTemperatureMinDown?.isEnabled = false
                                    buttonTemperatureMaxUp?.isEnabled = false
                                    buttonTemperatureMaxDown?.isEnabled = false
                                    linearLayoutTemperature?.setBackgroundColor(Color.parseColor("#E8E8E8"))
                                }
                            }
                            "sensor_noise" -> {
                                Storage.Selected_n_sensor_id = it.id
                                sensorNoiseMinValue?.setText(String.format(Locale.ROOT, "%.1f", it.minvalue))
                                sensorNoiseMaxValue?.setText(String.format(Locale.ROOT, "%.1f", it.maxvalue))
                                if(it.isworking) {
                                    sensorNoiseMinValue?.isEnabled = true
                                    sensorNoiseMaxValue?.isEnabled = true
                                    imageWorkingNoise?.setImageResource(R.drawable.check_mark_svg_1)
                                    switchNoise?.isChecked = true
                                    buttonNoiseMinUp?.isEnabled = true
                                    buttonNoiseMinDown?.isEnabled = true
                                    buttonNoiseMaxUp?.isEnabled = true
                                    buttonNoiseMaxDown?.isEnabled = true
                                    linearLayoutNoise?.setBackgroundColor(Color.parseColor("#FFFFFF"))
                                } else {
                                    sensorNoiseMinValue?.isEnabled = false
                                    sensorNoiseMaxValue?.isEnabled = false
                                    imageWorkingNoise?.setImageResource(R.drawable.close_square_svg)
                                    switchNoise?.isChecked = false
                                    buttonNoiseMinUp?.isEnabled = false
                                    buttonNoiseMinDown?.isEnabled = false
                                    buttonNoiseMaxUp?.isEnabled = false
                                    buttonNoiseMaxDown?.isEnabled = false
                                    linearLayoutNoise?.setBackgroundColor(Color.parseColor("#E8E8E8"))
                                }
                            }
                            "sensor_humidity" -> {
                                Storage.Selected_h_sensor_id = it.id
                                sensorHumidityMinValue?.setText(String.format(Locale.ROOT, "%.1f", it.minvalue))
                                sensorHumidityMaxValue?.setText(String.format(Locale.ROOT, "%.1f", it.maxvalue))
                                if(it.isworking) {
                                    sensorHumidityMinValue?.isEnabled = true
                                    sensorHumidityMaxValue?.isEnabled = true
                                    imageWorkingHumidity?.setImageResource(R.drawable.check_mark_svg_1)
                                    switchHumidity?.isChecked = true
                                    buttonHumidityMinUp?.isEnabled = true
                                    buttonHumidityMinDown?.isEnabled = true
                                    buttonHumidityMaxUp?.isEnabled = true
                                    buttonHumidityMaxDown?.isEnabled = true
                                    linearLayoutHumidity?.setBackgroundColor(Color.parseColor("#FFFFFF"))
                                } else {
                                    sensorHumidityMinValue?.isEnabled = false
                                    sensorHumidityMaxValue?.isEnabled = false
                                    imageWorkingHumidity?.setImageResource(R.drawable.close_square_svg)
                                    switchHumidity?.isChecked = false
                                    buttonHumidityMinUp?.isEnabled = false
                                    buttonHumidityMinDown?.isEnabled = false
                                    buttonHumidityMaxUp?.isEnabled = false
                                    buttonHumidityMaxDown?.isEnabled = false
                                    linearLayoutHumidity?.setBackgroundColor(Color.parseColor("#E8E8E8"))
                                }
                            }
                        }
                    }
                    textBaseStationName?.text = data[0].basestationname
                    textBaseStationSerialNum?.text = data[0].basestationserialnumber

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Сенсорів не знайдено", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "У цього користувача не існуєє таких сенсорів 400", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні сенсорів1", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<SensorsGetResponce>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні сенсорів2", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun numPlusAndConvertToStr(num: String, plus: Double) : String {
        var res: String = ""
        try {
            val numDouble: Double = num.toDouble()
            val numRoundedPluced = String.format(Locale.ROOT, "%.1f", numDouble).toDouble().plus(0.1)
            res = String.format(Locale.ROOT, "%.1f", numRoundedPluced).toString()
        } catch (e: Exception) {
        }
        return res
    }

    private fun numMinusAndConvertToStr(num: String, plus: Double) : String {
        var res: String = ""
        try {
            val numDouble: Double = num.toDouble()
            val numRoundedPluced = String.format(Locale.ROOT, "%.1f", numDouble).toDouble().minus(0.1)
            res = String.format(Locale.ROOT, "%.1f", numRoundedPluced).toString()
        } catch (e: Exception) {
        }
        return res
    }

}