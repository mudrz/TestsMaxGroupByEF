# Max and Group by tests

Test max for different SQL providers

To start

```shell
dotnet watch run
```

## API URLs

### Max

```shell
curl localhost:5005/api/max/1
```

### GroupBy

```shell
curl localhost:5005/api/group/1
```

## DB Drop

```shell
dotnet ef database drop --context=LiteContext
```

```shell
dotnet ef database drop --context=PostgreContext
```
