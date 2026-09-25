// Official Microsoft web installer, downloaded and Authenticode-verified on 2026-09-25.
// When updating this URL, verify Microsoft's signature and update the SHA-256 together.
const
  DotNetUrl = 'https://download.visualstudio.microsoft.com/download/pr/2d6bb6b2-226a-4baa-bdec-798822606ff1/9b7b8746971ed51a1770ae4293618187/ndp48-web.exe';
  DotNetFileName = 'ndp48-web.exe';
  DotNetSHA256 = '0bba3094588c4bfec301939985222a20b340bf03431563dec8b2b4478b06fffa';

var
  DotNetDownloadPage: TDownloadWizardPage;
  DotNetInstalledThisSession: Boolean;
  DotNetRestartRequired: Boolean;

procedure InitializeWizard;
begin
  DotNetDownloadPage := CreateDownloadPage(CustomMessage('DotNetTitle'),
    CustomMessage('DotNetDescription'), nil);
  DotNetDownloadPage.ShowBaseNameInsteadOfUrl := True;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo,
  MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
begin
  Result := MemoDirInfo + NewLine + MemoGroupInfo + NewLine + MemoTasksInfo;
  if not IsDotNetInstalled(net48, 0) then
    Result := Result + NewLine + NewLine + CustomMessage('DotNetReady');
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  ResultCode: Integer;
begin
  Result := '';
  if DotNetInstalledThisSession or IsDotNetInstalled(net48, 0) then
    Exit;

  DotNetDownloadPage.Clear;
  DotNetDownloadPage.Add(DotNetUrl, DotNetFileName, DotNetSHA256);
  DotNetDownloadPage.Show;
  try
    try
      DotNetDownloadPage.Download;
    except
      if DotNetDownloadPage.AbortedByUser then
        Result := CustomMessage('DotNetCancelled')
      else
        Result := FmtMessage(CustomMessage('DotNetDownloadFailed'), [GetExceptionMessage]);
    end;
  finally
    DotNetDownloadPage.Hide;
  end;
  if Result <> '' then
    Exit;

  // Elevate only Microsoft's installer, retaining the original user's app/data directory.
  // /passive shows progress; /norestart leaves restart control to our final wizard page.
  WizardForm.PreparingLabel.Caption := CustomMessage('DotNetTitle');
  if not ShellExec('runas', ExpandConstant('{tmp}\') + DotNetFileName,
    '/passive /norestart', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode) then
  begin
    Result := FmtMessage(CustomMessage('DotNetLaunchFailed'), [SysErrorMessage(ResultCode)]);
    Exit;
  end;

  Log(Format('.NET Framework installer exit code: %d', [ResultCode]));
  if (ResultCode = 3010) or (ResultCode = 1641) then
  begin
    // Microsoft reports success with a restart needed. Do not launch the app yet.
    DotNetRestartRequired := True;
    DotNetInstalledThisSession := True;
  end
  else if ResultCode <> 0 then
    Result := FmtMessage(CustomMessage('DotNetInstallFailed'), [IntToStr(ResultCode)])
  else if not IsDotNetInstalled(net48, 0) then
    Result := CustomMessage('DotNetNotDetected')
  else
    DotNetInstalledThisSession := True;
end;

function NeedRestart: Boolean;
begin
  Result := DotNetRestartRequired;
end;
