using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    float[] m_audioSpectrum;
    public static float spectrumValue;


    // Start is called before the first frame update
    void Start()
    {
        m_audioSpectrum = new float[256];
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }

    }
}
