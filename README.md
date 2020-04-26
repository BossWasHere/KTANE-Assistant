# KTANE-Assistant v0.1.0
#### By BackwardsNode/BossWasHere
> This is a **WIP project**<br>
Complete modules: Keypads<br>
Functioning modules: Simple Wires, The Button, Morse Code, Passwords

Interactive bomb defusing guide and assistant for [Keep Talking and Nobody Explodes](https://keeptalkinggame.com/) by Steel Crate Games. This enables "the expert" to enter information about the bomb, and get detailed instructions back for each module, essentially making this a "cheat". It is intended to be used to improve communication between the player and the expert or for practice on harder modules.
The original manual for the game can be found [here](http://www.bombmanual.com/).


### Building
To build, WPF is required. No additional dependencies need to be installed. For developing custom module pages, read **Developing**.

### Developing
A few simple classes are provided for registering module pages with the application. Other than that, create your pages as you usually would with WPF.
The entry point of your library should look like this:
```C#
...
public class MyModuleProvider : ExternalModuleProvider
{
	public override string ProjectAuthor => "BackwardsNode";
	public override string ProjectAuthorWebsite => "https://github.com";
	public override void Loaded() {}
	public override void RegistrationOpened()
	{
		ModuleManager.RegisterModule(new MyModule());
	}
	public override void Unloaded() {}
}
...
```
Follow the implementations set out by the API classes. Any public classes which inherit `ExternalModuleProvider` will be loaded. Only when `Loaded()` is called is it safe to interact between different modules.

### Contributing
Any issues should be submitted to this repository.