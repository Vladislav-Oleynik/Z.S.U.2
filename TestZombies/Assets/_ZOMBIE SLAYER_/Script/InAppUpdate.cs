using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.AppUpdate;
using Google.Play.Common;
using UnityEngine.UI;

public class InAppUpdate : MonoBehaviour
{
    [SerializeField] private Text inAppStatus;
    AppUpdateManager appUpdateManager;
    private void Start()
    {
        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckForUpdate());
    }
    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();

        //wait until the async operation completes

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                inAppStatus.text = UpdateAvailability.UpdateAvailable.ToString();
            }
            else
            {
                inAppStatus.text = "No update available.";
            }

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
    }

    private IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoOp_i, appUpdateOptions_i);

        yield return startUpdateRequest;
    }
}
