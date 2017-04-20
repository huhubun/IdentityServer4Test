using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            // entity to model
            CreateMap<Client, ClientViewModel>(MemberList.Destination)
                .ForMember(src => src.AllowedGrantTypes,
                    opt => opt.MapFrom(src => src.AllowedGrantTypes.ToDictionary(t => t.Id, t => t.GrantType)))
                .ForMember(src => src.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.ToDictionary(t => t.Id, t => t.RedirectUri)))
                .ForMember(src => src.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.ToDictionary(t => t.Id, t => t.PostLogoutRedirectUri)))
                .ForMember(src => src.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.ToDictionary(t => t.Id, t => t.Scope)))
                //.ForMember(x => x.ClientSecrets, opt => opt.MapFrom(src => src.ClientSecrets.Select(x => x)))
                //.ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))))
                .ForMember(src => src.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.ToDictionary(t => t.Id, t => t.Provider)))
                .ForMember(src => src.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.ToDictionary(t => t.Id, t => t.Origin)));

            CreateMap<ClientSecret, ClientSecretViewModel>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null));

            // model to entity
            CreateMap<ClientViewModel, Client>(MemberList.Source)
                .ForMember(dest => dest.AllowedGrantTypes, opt => opt.Ignore())
                .ForMember(dest => dest.RedirectUris, opt => opt.Ignore())
                .ForMember(dest => dest.PostLogoutRedirectUris, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.AllowedGrantTypes = dest.AllowedGrantTypes.AddOrRemoveFrom(
                        src.AllowedGrantTypes,
                        (s, d) => s.Key == d.Id,
                        s => s.Key == 0,
                        s => new ClientGrantType { GrantType = s.Value, Client = new Client { Id = src.Id } }
                    ).ToList();


                    //// AllowedGrantTypes
                    //var allowedGrantTypesRemoveItems = new List<ClientGrantType>();
                    //var allowedGrantTypesAddItems = src.AllowedGrantTypes.Where(p => p.Key == 0).Select(p => new ClientGrantType { Id = p.Key, GrantType = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.AllowedGrantTypes)
                    //{
                    //    if (!src.AllowedGrantTypes.Any(p => p.Key == destItem.Id))
                    //    {
                    //        allowedGrantTypesRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in allowedGrantTypesRemoveItems)
                    //{
                    //    dest.AllowedGrantTypes.Remove(item);
                    //}

                    //dest.AllowedGrantTypes.AddRange(allowedGrantTypesAddItems);

                    //// RedirectUris
                    //var redirectUrisRemoveItems = new List<ClientRedirectUri>();
                    //var redirectUrisAddItems = src.RedirectUris.Where(p => p.Key == 0).Select(p => new ClientRedirectUri { Id = p.Key, RedirectUri = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.RedirectUris)
                    //{
                    //    if (!src.RedirectUris.Any(p => p.Key == destItem.Id))
                    //    {
                    //        redirectUrisRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in redirectUrisRemoveItems)
                    //{
                    //    dest.RedirectUris.Remove(item);
                    //}

                    //dest.RedirectUris.AddRange(redirectUrisAddItems);

                    //// PostLogoutRedirectUris
                    //var postLogoutRedirectUrisRemoveItems = new List<ClientPostLogoutRedirectUri>();
                    //var postLogoutRedirectUrisAddItems = src.PostLogoutRedirectUris.Where(p => p.Key == 0).Select(p => new ClientPostLogoutRedirectUri { Id = p.Key, PostLogoutRedirectUri = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.PostLogoutRedirectUris)
                    //{
                    //    if (!src.PostLogoutRedirectUris.Any(p => p.Key == destItem.Id))
                    //    {
                    //        postLogoutRedirectUrisRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in postLogoutRedirectUrisRemoveItems)
                    //{
                    //    dest.PostLogoutRedirectUris.Remove(item);
                    //}

                    //dest.PostLogoutRedirectUris.AddRange(postLogoutRedirectUrisAddItems);

                    //// AllowedScopes
                    //var allowedScopesRemoveItems = new List<ClientScope>();
                    //var allowedScopesAddItems = src.AllowedScopes.Where(p => p.Key == 0).Select(p => new ClientScope { Id = p.Key, Scope = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.AllowedScopes)
                    //{
                    //    if (!src.AllowedScopes.Any(p => p.Key == destItem.Id))
                    //    {
                    //        allowedScopesRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in allowedScopesRemoveItems)
                    //{
                    //    dest.AllowedScopes.Remove(item);
                    //}

                    //dest.AllowedScopes.AddRange(allowedScopesAddItems);

                    //// IdentityProviderRestrictions
                    //var identityProviderRestrictionsRemoveItems = new List<ClientIdPRestriction>();
                    //var identityProviderRestrictionsAddItems = src.IdentityProviderRestrictions.Where(p => p.Key == 0).Select(p => new ClientIdPRestriction { Id = p.Key, Provider = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.IdentityProviderRestrictions)
                    //{
                    //    if (!src.IdentityProviderRestrictions.Any(p => p.Key == destItem.Id))
                    //    {
                    //        identityProviderRestrictionsRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in identityProviderRestrictionsRemoveItems)
                    //{
                    //    dest.IdentityProviderRestrictions.Remove(item);
                    //}

                    //dest.IdentityProviderRestrictions.AddRange(identityProviderRestrictionsAddItems);

                    //// AllowedCorsOrigins
                    //var allowedCorsOriginsRemoveItems = new List<ClientCorsOrigin>();
                    //var allowedCorsOriginsAddItems = src.AllowedCorsOrigins.Where(p => p.Key == 0).Select(p => new ClientCorsOrigin { Id = p.Key, Origin = p.Value, Client = new Client { Id = src.Id } });

                    //foreach (var destItem in dest.AllowedCorsOrigins)
                    //{
                    //    if (!src.AllowedCorsOrigins.Any(p => p.Key == destItem.Id))
                    //    {
                    //        allowedCorsOriginsRemoveItems.Add(destItem);
                    //    }
                    //}

                    //foreach (var item in allowedCorsOriginsRemoveItems)
                    //{
                    //    dest.AllowedCorsOrigins.Remove(item);
                    //}

                    //dest.AllowedCorsOrigins.AddRange(allowedCorsOriginsAddItems);

                });

            CreateMap<ClientSecretViewModel, ClientSecret>(MemberList.Source);


        }
    }

    public static class A
    {
        public static IEnumerable<TDestination> AddOrRemoveFrom<TSource, TDestination>(this List<TDestination> destinations, List<TSource> sources, Func<TSource, TDestination, bool> removeItemsWhere, Func<TSource, bool> newItemsWhere, Func<TSource, TDestination> createDestExp)
        {
            if (sources == null)
            {
                sources = new List<TSource>();
            }

            var removeItems = new List<TDestination>();
            var addItems = sources.Where(newItemsWhere).Select(createDestExp);

            foreach (var destItem in destinations)
            {
                if (!sources.Any(src => removeItemsWhere(src, destItem)))
                {
                    removeItems.Add(destItem);
                }
            }

            foreach (var item in removeItems)
            {
                destinations.Remove(item);
            }

            // TODO Update ?

            destinations.AddRange(addItems);


            return destinations;
        }
    }
}
