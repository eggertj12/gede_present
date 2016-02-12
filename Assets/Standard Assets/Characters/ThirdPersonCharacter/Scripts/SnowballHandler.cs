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
		private bool m_hasHit = false;

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

			if (m_hasHit) {
				return;
			}
			m_hasHit = true;

			if (LayerMask.NameToLayer("Player") == other.gameObject.layer)
			{
				Debug.LogWarning ("Hitting player ");

				var script = other.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter> ();
				script.hit ();

			}
			else
			{
				Debug.LogWarning ("Hitting something else");
			}

			m_SnowballHitParticles = gameObject.GetComponentInChildren<ParticleSystem>();

			gameObject.GetComponent<Rigidbody> ().isKinematic = true;

			gameObject.transform.localScale = Vector3.zero;

			m_SnowballHitParticles.transform.rotation = Quaternion.identity;
			m_SnowballHitParticles.Play ();

			Destroy (gameObject, m_SnowballHitParticles.duration);
		}


		// Update is called once per frame
		void Update () {
		
		}
	}
}