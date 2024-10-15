using System;
using UniGLTF;
using UnityEngine;

namespace UniVRM10.VRM10Viewer
{
    class Loaded : IDisposable
    {
        RuntimeGltfInstance m_instance;
        Vrm10Instance m_controller;
        public Vrm10Instance Instance => m_controller;
        public Vrm10RuntimeControlRig ControlRig => m_controller.Runtime.ControlRig;
        public Vrm10Runtime Runtime => m_controller.Runtime;

        public Loaded(RuntimeGltfInstance instance, Transform lookAtTarget)
        {
            m_instance = instance;

            m_controller = instance.GetComponent<Vrm10Instance>();
            if (m_controller != null)
            {
                // VRM
                m_controller.UpdateType = Vrm10Instance.UpdateTypes.LateUpdate; // after HumanPoseTransfer's setPose

            }

            var animation = instance.GetComponent<Animation>();
            if (animation && animation.clip != null)
            {
                // GLTF animation
                animation.Play(animation.clip.name);
            }
        }

        public void Dispose()
        {
            // destroy GameObject
            GameObject.Destroy(m_instance.gameObject);
        }
    }
}
