using Microsoft.EntityFrameworkCore;
using Sisat.Models;
using SQLitePCL;
using System;
using System.Linq;

namespace Sisat.ViewModels
{
    public class BaseViewModel
    {
        public ProjetoListViewModel ProjetoListViewModel { get; set; }
        public IEnumerable<PacotesAtualizacoes> PacotesAtualizacoes { get; set; }

        public static Usuario? UsuarioMemoria { get; set; }
        public Usuario? Usuario => UsuarioMemoria;


        public List<ProjetoComUltimoPacote> HomeProjeto()
        {
            var idProj = (this.Usuario.Conveniados.FirstOrDefault() ?? new()).ConvenioProjeto.Select(c => c.IdProj).ToList();

            using (var _context = new SisatContext())
            {
                var projetosComUltimoPacote = _context.Projetos.Where(ip => (this.Usuario.IdNivAcesso == 1 ? true : idProj.Contains(ip.IdProjeto)))
                    .Select(proj => new ProjetoComUltimoPacote
                    {
                        Projeto = proj,
                        UltimoPacote = _context.PacotesAtualizacoes
                            .Where(pacote => pacote.IdProj == proj.IdProjeto)
                            .OrderByDescending(pacote => pacote.NumVersao)
                            .FirstOrDefault()
                    }).ToList();

                return projetosComUltimoPacote;
            }
        }

        public List<RetornaChats> ListaChats()
        {

            using (var _context = new SisatContext())
            {
                var listaMensagens = _context.Chat
                    .Include(c => c.IdRemetenteNavigation)
                    .ThenInclude(c => c.Conveniados)
                    .Where(c => c.IdDestinatario == 1 || c.IdRemetente == 2)
                    .GroupBy(c => c.IdRemetente)
                    .Select(grupo => new RetornaChats
                    {
                        Chats = grupo.OrderByDescending(c => c.DataEnvio).FirstOrDefault(),
                        RetornaChat = grupo.OrderByDescending(c => c.DataEnvio).Skip(1).FirstOrDefault(),
                    })
                    .ToList();

                return listaMensagens;
            }
        }

        public List<ChatView> ListaIds()
        {
            using (var _context = new SisatContext())
            {
                var listaIdSelecionado = _context.Chat
                    .Include(d => d.IdDestinatarioNavigation)
                    .Where(c => c.IdDestinatario != 1 || c.IdRemetente == 2)
                    .GroupBy(d => d.IdDestinatario)
                    .Select(dest => new ChatView
                    {
                        IdSelecionado = dest.OrderByDescending(c => c.DataEnvio).FirstOrDefault(),

                    }).ToList();

                return listaIdSelecionado;
            }
          
        }

        public void Logout(HttpContext httpContext)
        {
            UsuarioMemoria = null;
            httpContext.Response.Redirect("/Index/Login");
        }
    }

    public class ChatView
    {
        public Chat IdSelecionado { get; set; }

    }

    public class RetornaChats
    {
        public Chat Chats { get; set; }

        public Chat RetornaChat { get; set; }
    }

    public class ProjetoComUltimoPacote
    {
        public Projetos Projeto { get; set; }

        public PacotesAtualizacoes UltimoPacote { get; set; }

        public PacotesAtualizacoes RetornoPacote { get; set; }
        public PacotesAtualizacoes HomeUltimoPacote { get; set; }


    }
}
