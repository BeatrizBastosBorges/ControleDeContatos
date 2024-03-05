using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio,
                                      ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenha)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenha.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenha);

                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";

                    return View("Index", alterarSenha);
                }

                return View("Index", alterarSenha);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamente, detalhes do erro: {erro.Message}";
                return View("Index", alterarSenha);
            }
        }
    }
}
