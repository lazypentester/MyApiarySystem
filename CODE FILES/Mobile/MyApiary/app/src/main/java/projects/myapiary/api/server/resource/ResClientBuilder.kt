package projects.myapiary.api.server.resource

import com.google.gson.Gson
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.converter.scalars.ScalarsConverterFactory
import java.util.concurrent.TimeUnit

object ResClientBuilder {
    private const val URL = "http://verabryzgalova-001-site1.etempurl.com/api/"
    //val contentType = "application/json".toMediaType()
    private val logger = HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY)
    private val okHttp = OkHttpClient.Builder().connectTimeout(30, TimeUnit.SECONDS).readTimeout(30, TimeUnit.SECONDS).writeTimeout(30, TimeUnit.SECONDS).addInterceptor(logger)
    private val builder = Retrofit.Builder().baseUrl(URL)
        //.addConverterFactory(ScalarsConverterFactory.create())
        .addConverterFactory(GsonConverterFactory.create())
        .client(okHttp.build())
    private val retrofit = builder.build()

    fun <T> buildClient(serviceType: Class<T>): T {
        return retrofit.create(serviceType)
    }
}