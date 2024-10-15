using CA.PlayerController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniGLTF;
using UnityEditor.Animations;
using UnityEngine;
using UniVRM10;
using UniVRM10.URPSample;
using UniVRM10.VRM10Viewer;

public class VRMModelImporter : MonoBehaviour
{
    private Vrm10Instance _loadedVrm;

    Loaded m_loaded;

    private CancellationTokenSource _cancellationTokenSource;

    [SerializeField]
    GameObject m_target = default;

    [SerializeField]
    public AnimatorController GlobalAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void OnOpenModelButtonClicked()
    {
        if (_loadedVrm)
        {
            Destroy(_loadedVrm.gameObject);
        }

#if UNITY_STANDALONE_WIN
        var path = FileDialogForWindows.FileDialog("open VRM", "vrm");
#elif UNITY_EDITOR
    var path = UnityEditor.EditorUtility.OpenFilePanel("Open VRM", "", "vrm");
#else
    var path = Application.dataPath + "/default.vrm";
#endif
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        // Cleanup previous load
        m_loaded?.Dispose();
        m_loaded = null;
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;

        try
        {
            Debug.LogFormat("{0}", path);
            var vrm10Instance = await Vrm10.LoadPathAsync(path,
                canLoadVrm0X: true,
                showMeshes: false,
                ct: cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                UnityObjectDestroyer.DestroyRuntimeOrEditor(vrm10Instance.gameObject);
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (vrm10Instance == null)
            {
                Debug.LogWarning("LoadPathAsync is null");
                return;
            }

            var instance = vrm10Instance.GetComponent<RuntimeGltfInstance>();
            instance.ShowMeshes();
            instance.EnableUpdateWhenOffscreen();

            // Swap the meshes from VRM to m_target
            SwapMeshes(vrm10Instance.gameObject, m_target);

            m_loaded = new Loaded(instance, m_target.transform);
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                Debug.LogWarning($"Canceled to Load: {path}");
            }
            else
            {
                Debug.LogError($"Failed to Load: {path}");
                Debug.LogException(ex);
            }
        }
    }

    private void SwapMeshes(GameObject importedVRM, GameObject target)
    {
        // Store references to all components attached to the target
        Component[] targetComponents = target.GetComponents<Component>();

        // Remove old meshes/children from m_target
        foreach (Transform child in target.transform)
        {
            Destroy(child.gameObject);
        }

        // Move VRM model to target position
        importedVRM.transform.SetParent(target.transform);
        importedVRM.transform.localPosition = Vector3.zero;
        importedVRM.transform.localRotation = Quaternion.identity;

        ReplacePlayerAnimator(importedVRM, target);
    }

    // Reference to the PlayerAnimator controller (assign this in the Inspector or dynamically)
    [SerializeField]
    private RuntimeAnimatorController playerAnimatorController;

    private void ReplacePlayerAnimator(GameObject importedVRM, GameObject playerObject)
    {
        // Find the new Animator on the imported VRM model
        Animator importedAnimator = importedVRM.GetComponent<Animator>();

        if (importedAnimator != null)
        {
            // Get the PlayerAnimation script first
            PlayerAnimation playerAnimation = playerObject.GetComponent<PlayerAnimation>();

            // Add the new Animator component from the imported VRM model to the Player object
            Animator newAnimator = importedVRM.GetComponent<Animator>();

            // Now assign the new Animator to the PlayerAnimation script
            if (playerAnimation != null)
            {
                playerAnimation._animator = newAnimator; // Assign the new Animator to the PlayerAnimation script
                newAnimator.runtimeAnimatorController = GlobalAnimation;
            }
            else
            {
                Debug.LogWarning("PlayerAnimation script not found on the Player object.");
            }
        }
        else
        {
            Debug.LogWarning("Animator component not found on the imported VRM model.");
        }
    }




}
