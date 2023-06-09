<template>
    <div class="main-layout">
        <SiteHeader></SiteHeader>
        <div class="main-container">
            <LeftMenu></LeftMenu>
            <div class="content">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
    import SiteHeader from './SiteHeader.vue';
    import LeftMenu from './LeftMenu.vue';

    export default{
    components: {
        SiteHeader,
        LeftMenu
    }
    };
</script>

<style>
.main-container {
    height: 100%;
}
 .main-layout {
    height: 100vh;
    display: flex;
    flex-direction: column;
 }
 .content {
    display: inline-flex;
    padding: 15px;
 }
</style>