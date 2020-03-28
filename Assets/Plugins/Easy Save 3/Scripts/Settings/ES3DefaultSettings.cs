﻿using UnityEngine;

namespace ES3Internal
{
	public class ES3DefaultSettings : MonoBehaviour
	{
		[SerializeField]
		public ES3SerializableSettings settings = null;

		public bool addMgrToSceneAutomatically = true;
		public bool autoUpdateReferences = true;
	}
}