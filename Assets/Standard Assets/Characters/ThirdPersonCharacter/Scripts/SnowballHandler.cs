using UnityEngine;
using System.Collections;

namespace WhatTheFuck
{
	public class SnowballHandler : MonoBehaviour {

		public float m_MaxDamage = 100f;                  
		public float m_ExplosionForce = 1000f;            
		public float m_MaxLifeTime = 5f;                  
		public float m_ExplosionRadius = 5f;
		public GameObject m_SnowballHitPrefab;
		[HideInInspector] public bool m_isThrown = false;

		private ParticleSystem m_SnowballHitParticles;

		// Use this for initialization
		void Start () {
//			Debug.LogWarning ("parent " + gameObject.name);
//			m_SnowballHitParticles.transform.position = new Vector3 (0, 0, 0);
//			m_SnowballHitParticles.transform.parent = gameObject.transform;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!m_isThrown) {
				return;
			}
			Debug.LogWarning ("Hitting something");

			m_SnowballHitParticles = gameObject.GetComponentInChildren<ParticleSystem>();
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;

//			m_SnowballHitParticles.transform.parent = null;
			m_SnowballHitParticles.Play ();

//			Destroy (m_SnowballHitParticles.gameObject, m_SnowballHitParticles.duration);

			Destroy (gameObject, m_SnowballHitParticles.duration);

			//		Destroy (gameObject);
		}


		// Update is called once per frame
		void Update () {
		
		}
	}
}