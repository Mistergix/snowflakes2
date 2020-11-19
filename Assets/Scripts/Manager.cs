using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisCity.Nicolas 
{
	public class Manager : MonoBehaviour
	{
		[SerializeField] private HexaGrid hexaGrid;
		[SerializeField] private Model model;
		[SerializeField] private View view;
		[SerializeField] private Text radiusText, gammaText, betaText;

		private bool showState;
		private int rayon;
		private float gamma, beta;

        private void Start()
        {
			rayon = 3;
        }

        public void SetRayon(float r)
        {
			rayon = (int)r;
			radiusText.text = r + "";
        }

		public void SetGamma(float g)
        {
			gamma = g;
			gammaText.text = g + "";
        }

		public void ShowState(bool val)
        {
			showState = val;
        }

		public void SetBeta(float b)
        {
			beta = b;
			betaText.text = b + "";
        }

		public void StartModel()
        {
			hexaGrid.Rayon = rayon;
			model.Gamma = gamma;
			model.Beta = beta;

			hexaGrid.ResetGrid(showState);
			model.ResetModel();
			view.StartModel();
        }
	}
}