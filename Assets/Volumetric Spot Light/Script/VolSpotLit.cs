using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolSpotLit : MonoBehaviour
{
	public Camera m_Eye;
	public enum EPattern { Flat = 0, DynamicMove, ColorfulBeam };
	public EPattern m_Pattern = EPattern.Flat;
	public Color m_SpotColor = Color.white;
	[Range(0f, 16f)] public float m_SpotAttenuation = 5f;
	[Range(0.1f, 8f)] public float m_SpotPower = 1.2f;
	[Header("Dynamic Move")]
	[Range(-2f, 0f)] public float m_NoiseBias = 0f;
	[Header("Colorful Beam")]
	[Range(1f, 2f)] public float m_BeamIntensity = 1f;
	[Range(0f, 1f)] public float m_BeamMove = 0.3f;
	private MeshRenderer m_LitVolRdr;
	private Transform m_LitVolTransform;
	private Taper m_LitVolTaper;
	private Shader m_SdrOutside;
	private Shader m_SdrInside;
	private bool m_SoftIntersection = true;
	private Noise3D m_Noise3D = new Noise3D ();

	void Start ()
	{
		m_Eye.depthTextureMode = DepthTextureMode.Depth;
		QualitySettings.antiAliasing = 8;
		
		GameObject litvol = GameObject.Find ("LightVolumetric");
		if (litvol != null)
		{
			m_LitVolRdr = litvol.GetComponent<MeshRenderer> ();
			m_LitVolTransform = litvol.GetComponent<Transform> ();
			m_LitVolTaper = litvol.GetComponent<Taper> ();
		}
		
		m_SdrOutside = Shader.Find ("Volumetric Spot Light/Outside");
		m_SdrInside = Shader.Find ("Volumetric Spot Light/Inside");
		
		ApplyShaderState ();
		m_Noise3D.Create (64, 2);
	}
	void Update ()
    {
		// fill spot light position
		GameObject litsrc = GameObject.Find ("LightSource");
		if (litsrc != null)
		{
			// make sure "LightSource" same height with "Taper" so "Spot Attenuation" works as expectation
			Vector3 np = new Vector3 (litsrc.transform.position.x, m_LitVolTaper.m_Height, litsrc.transform.position.z);
			litsrc.transform.position = np;
			m_LitVolRdr.material.SetVector ("_SpotPosition", litsrc.transform.position);
			m_LitVolRdr.material.SetColor ("_SpotColor", m_SpotColor);
			m_LitVolRdr.material.SetFloat ("_SpotAttenuation", m_SpotAttenuation);
			m_LitVolRdr.material.SetFloat ("_SpotAnglePower", m_SpotPower);
			m_LitVolRdr.material.SetFloat ("_NoiseBias", m_NoiseBias);
			m_LitVolRdr.material.SetFloat ("_BeamIntensity", m_BeamIntensity);
			m_LitVolRdr.material.SetFloat ("_BeamMove", m_BeamMove);
			m_LitVolRdr.material.SetTexture ("_NoiseTex", m_Noise3D.Get ());
		}
		// change inside / outside shader
		Vector3 eyePos = m_Eye.transform.position;
		Vector3 taperCenter = m_LitVolTransform.position;
		float taperRadius = m_LitVolTaper.m_RadiusStart - 0.5f;
		float dtX2 = (eyePos.x - taperCenter.x) * (eyePos.x - taperCenter.x);
		float dtZ2 = (eyePos.z - taperCenter.z) * (eyePos.z - taperCenter.z);
		if (dtX2 + dtZ2 < taperRadius * taperRadius)
			m_LitVolRdr.material.shader = m_SdrInside;
		else
			m_LitVolRdr.material.shader = m_SdrOutside;
    }
	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 250, 30), "Volumetric Spot Light Demo");
		m_SoftIntersection = GUI.Toggle (new Rect (10, 40, 250, 30), m_SoftIntersection, " Soft Intersection");
		ApplyShaderState ();
	}
	void ApplyShaderState ()
	{
		if (m_Pattern == EPattern.Flat)
		{
			Shader.DisableKeyword("VSL_NOISE_FOLLOW");
			Shader.DisableKeyword("VSL_COLOR_BEAM");
		}
		else if (m_Pattern == EPattern.DynamicMove)
		{
			Shader.EnableKeyword("VSL_NOISE_FOLLOW");
			Shader.DisableKeyword("VSL_COLOR_BEAM");
		}
		else if (m_Pattern == EPattern.ColorfulBeam)
		{
			Shader.EnableKeyword("VSL_COLOR_BEAM");
			Shader.DisableKeyword("VSL_NOISE_FOLLOW");
		}
			
		if (m_SoftIntersection)
			Shader.EnableKeyword("VSL_SOFT_INTERSECTION");
		else
			Shader.DisableKeyword("VSL_SOFT_INTERSECTION");
	}
}
