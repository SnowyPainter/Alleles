using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Alleles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//index.html의 id:app에 (다른 Shared/ 컴포넌트)담을 수 있는 
//MainLayout를 담은 페이지(이곳에서 NotFound/Found로 분기를 나눌수있음)
//App.razor(레이아웃 지정)를 렌더, MainLayout의 @Body에는 라우팅된 페이지가 렌더.
//@Body에는 Pages의 razor가 나옴.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
