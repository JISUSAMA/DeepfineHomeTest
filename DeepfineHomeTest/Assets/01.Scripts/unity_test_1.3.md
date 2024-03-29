구현한 과제 제출 방법

- 개인 GitHub repository를 public으로 생성 후 과제 unity 프로젝트를 업로드 하고 repository link 주소 전달


## 1. LoaderModule class 구현

``` C#
public class LoaderModule : MonoBehaviour
{
    public Action<GameObject> OnLoadCompleted;
    public void LoadAsset(string assetName)
    {
        // To do
    }
}

public class AssetLoader : MonoBehaviour
{
    [field: SerializeField]
    public LoaderModule LoaderModule { get; set; }

    private void Start()
    {
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }

    public void Load(string assetName)
    {
        LoaderModule.OnLoadCompleted += OnLoadCompleted;
        LoaderModule.LoadAsset(assetName);
    }

    private void OnLoadCompleted(GameObject loadedAsset)
    {
        loadedAsset.transform.SetParent(transform);
    }
}
```

위 AssetLoader class에서 LoaderModule.LoadAsset(assetName)을 호출해 3d model을 로딩하는 코드가 있습니다.
이 코드를 Unity GameObject에 Component로 추가 후
LoaderModule 내부 로직을 구현해 제시한 AssetLoader가 동작하도록 로직을 구현해 주세요.

- 제약 조건
  - 기능: 모델 파일은 obj 파일이며 최종적으로 play scene에 obj 파일이 동적 로드된 형태로 보여야 함.
  - 성능: 상식적인 수준의 모델 로딩 속도(5MB 파일 기준 1초 이내)와 렌더링 속도(60fps 이상)가 나와야 함
- 프로젝트 환경 설정 방법: Unity project 형태로 생성해서 Unity editor에서 열어서 바로 실행할 수 있는 형태여야 함
- 리뷰 포인트: LoaderModule에서 obj 파일 로드 구현 정확성 리뷰

## 2. LoaderModule 비동기 변경 구현

``` C#
public class LoaderModule : MonoBehaviour
{
    public Action<GameObject> OnLoadCompleted;
    public void LoadAsset(string assetName)
    {
        // To do
    }
    public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        // To do: create GameObject and obj model from assetName
        return new();
    }
}

public class AssetLoader : MonoBehaviour
{
    [field: SerializeField]
    public LoaderModule LoaderModule { get; set; }

    private void Start()
    {
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }

    public async void Load(string assetName)
    {
        GameObject loadedAsset = await LoaderModule.LoadAssetAsync(assetName);
        loadedAsset.transform.SetParent(transform);
    }
}
```

LoaderModule에서 LoadAssetAsync 메소드를 추가해 비동기 처리를 통해 obj 파일을 로드할 수 있도록 구현해 주세요.

- 제약 조건
  - 기능: 모델 파일은 obj 파일이며 최종적으로 play scene에 obj 파일이 동적 로드된 형태로 보여야 함.
  - 성능: 상식적인 수준의 모델 로딩 속도(5MB 파일 기준 1초 이내)와 렌더링 속도(60fps 이상)가 나와야 함
- 프로젝트 환경 설정 방법: Unity project 형태로 생성해서 Unity editor에서 열어서 바로 실행할 수 있는 형태여야 함
- 리뷰 포인트: 1번 구현과 비교해 성능 최적화 관점, 가독성 관점 등

## 3. 동시성 제어 구현

``` C#
public class LoaderModule : MonoBehaviour
{
    public Action<GameObject> OnLoadCompleted;
    public void LoadAsset(string assetName)
    {
        // To do
    }
    public Task<GameObject> LoadAssetAsync(assetName)
    {
        // To do
    }
}

public class AssetLoader : MonoBehaviour
{
    [field: SerializeField]
    public LoaderModule LoaderModule { get; set; }

    private void Start()
    {
        List<string> selectedAssetNames = GetObjFiles("/Resources/Models");
        Load(selectedAssetNames);
    }

    private List<string> GetObjFiles(string directory)
    {
        // To do
        return new();
    }

    public async void Load(List<string> assetNames)
    {
        // To do
    }
}
```

서로 다른 용량의 obj 파일 20개를 준비하고 LoaderModule.LoadAssetAsync()를 통해 obj 파일 20개를 동시에 로드할 수 있도록 코드를 변경해  주세요.

- 제약 조건
  - 기능: 모델 파일은 obj 파일이며 최종적으로 play scene에 obj 파일이 동적 로드된 형태로 보여야 함.
  - 성능: 상식적인 수준의 모델 로딩 속도(5MB 파일 기준 1초 이내)와 렌더링 속도(60fps 이상)가 나와야 함
  - 비동기 기능 기대 수준: 용량이 작은 모델은 먼저 로딩되고, 용량이 큰 모델은 나중에 로딩될 수 있어야 함
- 프로젝트 환경 설정 방법: Unity project 형태로 생성해서 Unity editor에서 열어서 바로 실행할 수 있는 형태여야 함
- 리뷰 포인트: 구현한 비동기 처리 코드에 대한 이해도 리뷰
