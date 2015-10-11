# .NET library for Lono Connected Sprinklers

[![Build status](https://ci.appveyor.com/api/projects/status/xnuqyd8bj4iaw173?svg=true)](https://ci.appveyor.com/project/jbct/lononet)

Lono is an internet-of-things connected sprinkler controller and one of the first outdoor 
smart home companies.  They can be found at [@lono-devices](//github.com/lono-devices).

This library is used to interface with Lono's public API. For more information, you can take
a look at their [documentation](http://make.lono.io/docs).

## Installation

The easiest way to install and use the LonoNet library is by using [NuGet](https://www.nuget.org/packages/LonoNet/).  You can also obtain the latest release binary by visiting the Appveyor site and looking for the DLL artifacts.

## How to use the library

Using the library is quite simple and should be familiar for you if you've ever used a REST-based library before.

##### Step 1

Visit [make.lono.io](http://make.lono.io) and get your developer client ID and client secret GUID's.

##### Step 2

Find your Lono controller's Device ID. If you have used the mobile app, you'll find it on My Account under Lono ID.

##### Step 3

Create a new application, reference the LonoNet.dll file and create a new instance of the LonoNetClient class:

```csharp
   var _client = new LonoNetClient("CLIENT ID", "CLIENT SECRET", "DEVICE ID");
```

##### Step 4

Build an authorization URL and launch the resulting URL in your default browser.

```csharp
   _client.BuildAuthorizationUrl("http://localhost", "write");
```

The first parameter specifies where you'll be redirected after authorization is successful and the second specifies the scope you wish to request access for.  "write" allows for full access.  See more details at the API site.

When you're redirected, you'll see

```html
   http://localhost/?code=<guid>
```

This resulting code is your auth code. You'll need that for the final step.

##### Step 5

Get your access token.

```csharp
   var login = _client.GetAccessToken("CODE FROM ABOVE");
```

##### Done

You're now able to query and enable/disable zones for your Lono sprinkler controller.  To enable or disable a zone, use the following command:

**Enables a zone**
```csharp
   _client.SetZone(1, true);
```

**Disables a zone**
```csharp
   _client.SetZone(1, false);
```

## Contributing

Bug reports and pull requests are welcome on Github at https://github.com/jbct/LonoNet

## License

This library is available as open source under the terms of the Apache 2.0 license.

## Credits

Thanks to the @DropNet project for a base by which to build this and to @lono-devices for building a great public API and controller!
