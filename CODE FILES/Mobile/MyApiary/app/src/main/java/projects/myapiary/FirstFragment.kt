package projects.myapiary

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.*
import android.view.ContextMenu.ContextMenuInfo
import android.widget.AdapterView
import android.widget.AdapterView.AdapterContextMenuInfo
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.google.gson.JsonObject
import projects.myapiary.adapters.ApiaryAdapter
import projects.myapiary.api.server.resource.ResClientBuilder
import projects.myapiary.api.server.resource.ResRequests
import projects.myapiary.databinding.FragmentFirstBinding
import projects.myapiary.models.Apiary
import projects.myapiary.storage.Storage
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class FirstFragment : Fragment() {

    // assign the _binding variable initially to null and
    // also when the view is destroyed again it has to be set to null

    // with the backing property of the kotlin we extract
    // the non null value of the _binding

    private var _binding: FragmentFirstBinding? = null
    private val binding get() = _binding!!

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {

        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        // inflate the layout and bind to the _binding
        _binding = FragmentFirstBinding.inflate(inflater, container, false)

        binding.buttonCreateNewApiary.setOnClickListener {
            val intent = Intent (context, ActivityNewApiaryScreen::class.java)
            requireActivity().startActivity(intent)
        }

        // register context menu
        registerForContextMenu(binding.apiariesList)

        // Get apiaries start

        getApiaries()

        // Get apiaries end

        return binding.root

    }

    /// START SET SETTINGS FOR CONTEXT MENU

    override fun onCreateContextMenu(menu: ContextMenu, v: View, menuInfo: ContextMenuInfo?) {
        super.onCreateContextMenu(menu, v, menuInfo)
        val inflater = requireActivity().menuInflater
        inflater.inflate(R.menu.context_menu_apiaries, menu)
    }

    override fun onContextItemSelected(item: MenuItem): Boolean {
        val info = item.menuInfo as AdapterContextMenuInfo
        var position_selected_item = info.position
        var apiary = binding.apiariesList.getItemAtPosition(position_selected_item) as Apiary
        when(item.itemId) {
            R.id.edit_apiary ->{
                val intent = Intent (context, ActivityUpdateApiaryScreen::class.java)
                intent.putExtra("id", apiary.id)
                intent.putExtra("name", apiary.Name)
                intent.putExtra("address", apiary.Address)
                intent.putExtra("userid", Storage.id)
                requireActivity().startActivity(intent)
                return true
            }
            R.id.delete_apiary ->{
                deleteApiary(apiary.id)
                return true
            }
        }
        return super.onContextItemSelected(item)
    }

    /// END SET SETTINGS FOR CONTEXT MENU


    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onResume() {
        super.onResume()
        getApiaries()
    }


    private fun getApiaries() {
        /// REQUEST FOR GET APIARIES

        val context = requireActivity().applicationContext

        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.getApiaries(Storage.id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<List<Apiary>> {
            override fun onResponse(
                call: Call<List<Apiary>>,
                response: Response<List<Apiary>>
            ) {
                if (response.isSuccessful) {
                    val data = response.body()!!

                    //var list = data.toList()
                    var list = data.toList()



                    binding.apiariesList.adapter = ApiaryAdapter(context, R.layout.apiary_row, list)

                    binding.apiariesList.setOnItemClickListener { parent: AdapterView<*>, view: View, position: Int, id: Long ->
                        Storage.Selected_apiary_id = list[position].id
                        val intent = Intent (context, ActivityBeehivesScreen::class.java)
                        requireActivity().startActivity(intent)
                    }

                } else {
                    when(response.code()) {
                        404 -> {
                            Toast.makeText(context, "Пасік не знайдено", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "Помилка при підрахунку праметрів пасік 400", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при виведенні пасік", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<Apiary>>, t: Throwable) {
                Toast.makeText(context, "Сталася помилка при виведенні пасік", Toast.LENGTH_SHORT).show()
            }
        })

        /// REQUEST FOR GET APIARIES END
    }

    private fun deleteApiary(apiary_id: Int) {
        val context = requireActivity().applicationContext

        val request = ResClientBuilder.buildClient(ResRequests::class.java)
        val response = request.deleteApiaries(apiary_id, "Bearer ${Storage.token_short}")

        response.enqueue(object: Callback<JsonObject> {
            override fun onResponse(call: Call<JsonObject>, response: Response<JsonObject>) {
                if (response.isSuccessful) {
                    val data = response.body()!!
                    Toast.makeText(requireActivity().applicationContext, "Пасіка видалена", Toast.LENGTH_SHORT).show()
                    getApiaries()
                } else {
                    when(response.code()) {
                        415 -> {
                            Toast.makeText(context, "Операція неможлива: за вуликами в пасіці вже закріплені датчики", Toast.LENGTH_SHORT).show()
                        }
                        404 -> {
                            Toast.makeText(context, "Пасік не знайдено за заданим ИД", Toast.LENGTH_SHORT).show()
                        }
                        400 -> {
                            Toast.makeText(context, "У цього користувача не існує такої пасіки", Toast.LENGTH_SHORT).show()
                        }
                        else -> {
                            Toast.makeText(context, "Сталася помилка при видаленні пасік1", Toast.LENGTH_SHORT).show()
                        }
                    }
                }
            }

            override fun onFailure(call: Call<JsonObject>, t: Throwable) {
                Toast.makeText(context, "Помилка при видаленні пасіки2", Toast.LENGTH_SHORT).show()
            }
        })
    }
}