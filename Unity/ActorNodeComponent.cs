using System;
using System.IO;
using UnityEngine;
using Nima.Math2D;

namespace Nima.Unity
{
	[ExecuteInEditMode]
	public class ActorNodeComponent : MonoBehaviour
	{
		protected ActorComponent m_ActorComponent;
		protected Nima.ActorNode m_ActorNode;

		public Nima.ActorNode Node
		{
			get
			{
				return m_ActorNode;
			}
		}

		public virtual void Initialize(ActorComponent actorComponent, Nima.ActorNode actorNode)
		{
			m_ActorComponent = actorComponent;
			m_ActorNode = actorNode;

			ActorNodeComponent parentComponent = actorComponent.Nodes[actorNode.ParentIdx];
			if(parentComponent == this)
			{
				// This is the root.
				// If the parent is self, we've reached root.
				gameObject.transform.parent = actorComponent.gameObject.transform;	
			}
			else
			{
				gameObject.transform.parent = parentComponent.gameObject.transform;
			}
			UpdateTransform();
		}

		public virtual void UpdateTransform()
		{
			if(m_ActorNode == null)
			{
				return;
			}
			m_ActorNode.UpdateTransforms();
			transform.localEulerAngles = new Vector3(0.0f, 0.0f, m_ActorNode.RenderRotation * Mathf.Rad2Deg);
			transform.localPosition = new Vector3(m_ActorNode.X, m_ActorNode.Y, 0.0f);
			transform.localScale = new Vector3(m_ActorNode.ScaleX, m_ActorNode.ScaleY, 1.0f);
			/*
			m_ActorNode.UpdateTransforms();
			Mat2D wt = m_ActorNode.WorldTransform;

			float x = wt[0];
			float y = wt[1];
			float scaleX = Mathf.Sqrt(x*x+y*y);

			x = wt[2];
			y = wt[3];
			float scaleY = Mathf.Sqrt(x*x+y*y);

			float rotation = Mathf.Atan2(wt[1], wt[0]);
			transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotation * Mathf.Rad2Deg);
			transform.localPosition = new Vector3(wt[4], wt[5], 0.0f);
			transform.localScale = new Vector3(scaleX, scaleY, 1.0f);*/
		}
	}
}