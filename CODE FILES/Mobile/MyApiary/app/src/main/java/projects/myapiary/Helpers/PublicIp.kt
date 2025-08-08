package projects.myapiary.Helpers

import android.content.Intent
import android.util.Log
import android.view.View
import android.widget.AdapterView
import android.widget.Toast
import com.google.gson.JsonObject
import projects.myapiary.ActivitySessionsScreen
import projects.myapiary.R
import projects.myapiary.adapters.DeviceAdapter
import projects.myapiary.api.server.ip.api64_ipify_org.IpifyClientBuilder
import projects.myapiary.api.server.ip.api64_ipify_org.IpifyRequests
import projects.myapiary.api.server.ip.ipregistry.IpregistryClientBuilder
import projects.myapiary.api.server.ip.ipregistry.IpregistryRequests
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.models.Device
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class PublicIp {
    companion object {
        fun GetFromIpify() {
            var ip = ""

            val request = IpifyClientBuilder.buildClient(IpifyRequests::class.java)
            val response = request.getPublicIpString()

            response.enqueue(object: Callback<String> {
                override fun onResponse(call: Call<String>, response: Response<String>) {
                    if (response.isSuccessful) {
                        val data = response.body()!!

                        ip = data.toString()

                        Storage.Session_Ip_address = ip;
                    }
                }

                override fun onFailure(call: Call<String>, t: Throwable) {
                    Log.println(Log.ASSERT, "Public IP", "Fail to get public IP")
                }
            })
        }

        fun GetInfoAboutClientIp() {
            var ip = ""

            val request = IpregistryClientBuilder.buildClient(IpregistryRequests::class.java)
            val response = request.getClientIpInfoJsonOriginIp()

            response.enqueue(object: Callback<JsonObject> {
                override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                    if (response.isSuccessful) {
                        val data = response.body()!!

                        var location = data.getAsJsonObject("location")

                        var country = location.getAsJsonObject("country").get("name").asString
                        var region = location.getAsJsonObject("region").get("name").asString
                        var city = location.get("city").asString

                        //Storage.Session_Geolocation = city.plus(", ").plus(region).plus(", ").plus(country)
                        Storage.Session_Geolocation = city.plus(", ").plus(country)
                    }
                }

                override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                    Log.println(Log.ASSERT, "Info Ip", "Fail to get info about IP")
                }
            })
        }
    }
}