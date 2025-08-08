package projects.myapiary

import android.content.Intent
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import projects.myapiary.databinding.FragmentFirstBinding
import projects.myapiary.databinding.FragmentSecondBinding

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [SecondFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class SecondFragment : Fragment() {

    private var _binding: FragmentSecondBinding? = null
    private val binding get() = _binding!!

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        // inflate the layout and bind to the _binding
        _binding = FragmentSecondBinding.inflate(inflater, container, false)

        binding.buttonProfile.setOnClickListener {
            val intent = Intent (context, ActivityProfileScreen::class.java)
            requireActivity().startActivity(intent)
        }

        binding.buttonSessions.setOnClickListener {
            val intent = Intent (context, ActivityDevicesScreen::class.java)
            requireActivity().startActivity(intent)
        }

        binding.buttonHelp.setOnClickListener {
            val intent = Intent (context, ActivityHelpScreen::class.java)
            requireActivity().startActivity(intent)
        }

        binding.buttonGoout.setOnClickListener {
            activity?.finish()
        }


        return binding.root
        /*// Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_second, container, false)*/
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}