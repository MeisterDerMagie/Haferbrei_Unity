namespace Haferbrei{
public interface IFloatModifierSource
{
    string SourceName { get; set; }
    
    //ILinkable link { get; set; } //als Idee: wenn man auf den Modifier klickt, kommt man zur Source. Dafür muss diese iwie verlinkt werden.
}
}